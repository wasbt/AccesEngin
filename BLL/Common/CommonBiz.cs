using DAL;
using ExcelDataReader;
using FirebaseNet.Messaging;
using log4net;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL.Common
{
    public class CommonBiz
    {
        public string RootFilesPath = ConfigurationManager.AppSettings["RootFilesPath"]?.ToString();
        public static string AzureStorageConnectionString = ConfigurationManager.ConnectionStrings["AzureStorageConnectionString"].ConnectionString;
        public static log4net.ILog log;
        public string FCMServerKey = ConfigurationManager.AppSettings["FCMServerKey"]?.ToString();
        public OcpPerformanceDataContext  context { get; set; }

        public CommonBiz(OcpPerformanceDataContext  context, ILog log)
        {
            this.context = context;
            log = log;
        }
        public static string GetUniqueNameForFileName(string fileName)
        {
            return Guid.NewGuid().ToString() + Path.GetExtension(fileName);
        }

        public string GetFullNameByUserId(string UserId)
        {
            var profil = context
                .Profile
                .Where(p => p.Id == UserId)
                .SingleOrDefault();

            if (profil != null)
            {
                return profil.FullName;
            }
            else
            {
                return "";
            }
        }

        public async Task<long> SaveAppFile(HttpPostedFileBase uploadedFile, string ContainerName, string SourceId, string SourceName)
        {
            try
            {
                if (uploadedFile?.ContentLength > 1)
                {
                    var SystemFileName = GetUniqueNameForFileName(uploadedFile.FileName);

                    #region Création du répértoire de destination
                    var DestinationDirectory = Path.Combine(RootFilesPath, ContainerName, SourceId);
                    if (!Directory.Exists(DestinationDirectory))
                        Directory.CreateDirectory(DestinationDirectory);
                    #endregion

                    #region Sauvegarde de la pièce jointe dans le système de fichiers
                    var FullFileName = Path.Combine(DestinationDirectory, SystemFileName);

                    uploadedFile.SaveAs(FullFileName);
                    #endregion

                    #region Ajout de la pièce jointe dans le contexte
                    var pj = new DAL.AppFile()
                    {
                        OriginalFileName = uploadedFile.FileName,   
                        FileSize = uploadedFile.ContentLength,
                        SystemFileName = FullFileName,
                        CreatedOn = DateTime.Now,
                        SourceId = SourceId,
                        SourceName = SourceName,
                        ContainerName = ContainerName
                    };

                    context.AppFile.Add(pj);
                    #endregion

                    await context.SaveChangesAsync();

                    return pj.AppFileId;
                }
                return 0;
            }
            catch (Exception ex)
            {
                log?.Error("Erreur lors de l'ajout de la pièce jointe", ex);

                await Task.Run(async () => { await MailHelper.SendEmail(new List<string>() { { ConstsAccesEngin.LogEmail } }, ex?.Message + "\n" + ex?.InnerException?.Message); });
                return 0;
            }
        }


        #region SendMail
        public static void SendEmail(List<string> To, string Subject, string Body, string mailFrom, List<string> ccList = null, string mailFromName = null, List<string> BCCList = null, List<string> files = null)
        {
            try
            {
                using (SmtpClient client = new SmtpClient())
                {
                    using (var mail = new MailMessage())
                    {
                        mail.From = new MailAddress(mailFrom, "ProjetGRI");
                        mail.HeadersEncoding = Encoding.UTF8;
                        mail.SubjectEncoding = Encoding.UTF8;
                        mail.BodyEncoding = Encoding.UTF8;
                        mail.Priority = MailPriority.High;
                        mail.Subject = Subject;
                        mail.Body = Body;
                        mail.IsBodyHtml = true;
                        #region foreach item in To?.Distinct()
                        foreach (var item in To?.Distinct())
                        {
                            mail.To.Add(item);
                        }
                        #endregion
                        #region ccList?.Count > 0
                        if (ccList?.Count > 0)
                        {
                            foreach (var ccEmail in ccList)
                            {
                                mail.CC.Add(ccEmail);
                            }
                        }
                        #endregion
                        #region BCCList?.Count > 0
                        if (BCCList?.Count > 0)
                        {
                            foreach (var bccEmail in BCCList)
                            {
                                mail.Bcc.Add(bccEmail);
                            }
                        }
                        #endregion
                        #region files?.Count > 0
                        if (files?.Count > 0)
                        {
                            foreach (var file in files)
                            {
                                mail.Attachments.Add(new Attachment(file));
                            }
                        }
                        #endregion
                        client.Send(mail);
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region Save File to Azure
        public byte[] ConvertToByte(HttpPostedFileBase file)
        {
            if (file == null)
                return null;

            byte[] imageByte = null;
            var rdr = new System.IO.BinaryReader(file.InputStream);
            imageByte = rdr.ReadBytes((int)file.ContentLength);
            return imageByte;
        }

        public static byte[] GetBytesFromUploadedFile(HttpPostedFileBase uploadedFile)

        {

            byte[] data;

            using (Stream inputStream = uploadedFile.InputStream)

            {

                MemoryStream memoryStream = inputStream as MemoryStream;

                if (memoryStream == null)

                {

                    memoryStream = new MemoryStream();

                    inputStream.CopyTo(memoryStream);

                }

                data = memoryStream.ToArray();

            }



            return data;

        }

        public static string UploadFileToBlob(string containerName, byte[] fileBytes, string fileName)
        {
            CloudStorageAccount account = CloudStorageAccount.Parse(AzureStorageConnectionString);
            CloudBlobClient client = account.CreateCloudBlobClient();
            CloudBlobContainer container = client.GetContainerReference(containerName);
            container.CreateIfNotExists();
            BlobContainerPermissions containerPermissions = new BlobContainerPermissions() { PublicAccess = BlobContainerPublicAccessType.Off };
            container.SetPermissions(containerPermissions);
            CloudBlockBlob file = container.GetBlockBlobReference(fileName);
            file.UploadFromByteArray(fileBytes, 0, fileBytes.Length);
            return file.Uri.ToString();
        }


        public async Task<long> SaveOCPFile(HttpPostedFileBase uploadedFile, string ContainerName, long SourceId, string SourceName)
        {
            try
            {

                if (uploadedFile?.ContentLength > 1)
                {
                    var SystemFileName = GetUniqueNameForFileName(uploadedFile.FileName);

                    var str = Convert.ToString(SourceId);

                    var uri = UploadFileToBlob(ContainerName?.ToLower(), GetBytesFromUploadedFile(uploadedFile), $"{str}/{SystemFileName}");

                    #region Ajout de la pièce jointe dans le contexte

                    var pj = new AppFile()
                    {
                        OriginalFileName = uploadedFile.FileName,
                        SystemFileName = uri,
                        CreatedOn = DateTime.Now,
                        SourceId = SourceId.ToString(),
                        SourceName = SourceName,
                        ContainerName = ContainerName
                    };

                    context.AppFile.Add(pj);
                    #endregion

                    await context.SaveChangesAsync();

                    return pj.AppFileId;
                }
                return 0;
            }
            catch (Exception ex)
            {
                log.Error("Erreur lors de l'ajout de la pièce jointe", ex);
                return 0;
            }


        }


        public async Task<Byte[]> GetBlobBytes(string uri, string containerName)

        {
            CloudStorageAccount account = CloudStorageAccount.Parse(AzureStorageConnectionString);

            CloudBlobClient client = account.CreateCloudBlobClient();

            CloudBlobContainer container = client.GetContainerReference(containerName);

            var blobs = container.ListBlobs(useFlatBlobListing: true);

            var blob = blobs.OfType<CloudBlob>().Where(x => x.Uri == new Uri(uri)).FirstOrDefault(); ;


            // CloudBlockBlob blob = container.GetBlockBlobReference(uri);


            await blob.FetchAttributesAsync();

            long fileByteLength = blob.Properties.Length;
            Byte[] myByteArray = new Byte[fileByteLength];
            await blob.DownloadToByteArrayAsync(myByteArray, 0);



            return myByteArray;

        }



        public static List<string> GetContainerBlobNames(string containerName)

        {

            CloudStorageAccount account = CloudStorageAccount.Parse(AzureStorageConnectionString);

            CloudBlobClient client = account.CreateCloudBlobClient();

            CloudBlobContainer container = client.GetContainerReference(containerName);



            var blobs = container.ListBlobs(useFlatBlobListing: true);

            var blobNames = blobs.OfType<CloudBlockBlob>().Select(b => b.Name).ToList();

            return blobNames;

        }


        public DataSet GetExcelBlobData(string filename, string containerName)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(AzureStorageConnectionString);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            // Retrieve reference to a blob named "test.xlsx"
            CloudBlockBlob blockBlobReference = container.GetBlockBlobReference(filename);

            DataSet ds;
            using (var memoryStream = new MemoryStream())
            {
                //downloads blob's content to a stream
                blockBlobReference.DownloadToStream(memoryStream);


                /*                 
                    used open source Excel Data Reader - Read Excel files in .NET(http://exceldatareader.codeplex.com/)
                    Nuget: Install-Package ExcelDataReader (https://www.nuget.org/packages/ExcelDataReader/)

                */

                var excelReader = ExcelReaderFactory.CreateOpenXmlReader(memoryStream);
                ds = excelReader.AsDataSet();
                excelReader.Close();
            }

            return ds;
        }
        #endregion

        #region Notifications
        public void AddNotification(string DestUserId, string Content, string SenderUserId, NotifTypes NotifType = NotifTypes.Info, long ObjectId = 0)
        {
            Notification n = new Notification();
            n.DtNotif = DateTime.Now;
            n.UserId = DestUserId;
            n.Content = Content;
            n.ObjectId = (int)ObjectId;
            n.SenderUserId = SenderUserId;

            if (NotifType == NotifTypes.Info)
            {
                n.ObjectType = "INFO";
            }
            else if (NotifType == NotifTypes.Activite)
            {
                n.ObjectType = "ACTIVITE";
            }
            else if (NotifType == NotifTypes.ActionInfo)
            {
                n.ObjectType = "ACTION";
            }
            else if (NotifType == NotifTypes.NewFollowRequest)
            {
                n.ObjectType = "NEWFOLLOW";
            }
            else if (NotifType == NotifTypes.FollowRequestAccepted)
            {
                n.ObjectType = "FOLLOW-ACCEPTED";
            }
            else if (NotifType == NotifTypes.FollowRequestDiscarded)
            {
                n.ObjectType = "FOLLOW-DISCARDED";
            }
            else if (NotifType == NotifTypes.HSE_NouvelleVose)
            {
                n.ObjectType = "NEW-VOSE";
            }
            else if (NotifType == NotifTypes.HSE_NouveauRapportFlash)
            {
                n.ObjectType = "NEW-FLASHREPORT";
            }
            else if (NotifType == NotifTypes.Fiabilite_AnalyseAFroid)
            {
                n.ObjectType = "FIABILITE-NEWACTION";
            }
            else if (NotifType == NotifTypes.Relance_Action)
            {
                n.ObjectType = "RELANCE-ACTION";
            }
            else if (NotifType == NotifTypes.AccesEngis)
            {
                n.ObjectType = "AccesEngis";
            }

            context.Notification.Add(n);
            context.SaveChanges();
        }
        #endregion

        public void SendGCMPushToUser(string title, string body, string userToken)
        {
            Task.Run(() =>
            {
                FCMClient client = new FCMClient(FCMServerKey);

                var message = new Message()
                {
                    To = userToken, //topic example /topics/all
                    Priority = MessagePriority.high,
                    Notification = new AndroidNotification()
                    {
                        Body = body,
                        Icon = "icon",
                        Sound = "default",
                        Title = title
                    },
                    Data = new Dictionary<string, string>
                    {
                      { "body", body },
                      { "title", title },
                      { "icon", "icon" },
                      { "target","news"},
                      { "sound","default"},
                    },
                };

                var result = client.SendMessageAsync(message).Result;
            });
        }

        public void SendGCMPushToTopics(string title, string body, string topics)
        {
            Task.Run(() =>
            {
                FCMClient client = new FCMClient(FCMServerKey);

                var message = new Message()
                {
                    Condition = topics,   //topic example /topics/all
                    Priority = MessagePriority.high,
                    Notification = new AndroidNotification()
                    {
                        Body = body,
                        Icon = "icon",
                        Sound = "default",
                        Title = title
                    },
                    Data = new Dictionary<string, string>
                    {
                      { "body", body },
                      { "title", title },
                      { "icon", "icon" },
                      { "target","news"},
                      { "sound","default"},
                    },
                };

                var result = client.SendMessageAsync(message).Result;
            });
        }
    }
}
