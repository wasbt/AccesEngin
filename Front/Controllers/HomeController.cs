using Front.ViewModels;
using Shared.API.IN;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DAL;
using BLL.Biz;
using Shared;
using Rotativa;
using BLL.Common;
using System.IO;
using System.Diagnostics;
using System.Web.Routing;
using Front.Models;
using X.PagedList;

namespace Front.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public async Task<ActionResult> DemandeExpired(StandardModel<DemandeAccesEngin> model)
        {

            var demandes = context
                .DemandeAccesEngin
                .AsQueryable();

            int pageSize = 10;

            int pageNumber = (model.page ?? 1);

            pageNumber = (model.newSearch ?? pageNumber);


            // add interval to today
            if (IsChefProjet)
            {

                demandes = demandes
                     .Where(x => 
                     x.StatutDemandeId == 3 &&  //==> expireé
                     x.CreatedBy == CurrentUserProfile.Id);
            }
            if (IsConroleur)
            {
                demandes = demandes
                     .Where(x => 
                     x.StatutDemandeId == 3); //==> expireé
            }




            demandes = demandes.OrderByDescending(x => x.CreatedOn);


            model.resultList = demandes.ToPagedList(pageNumber, pageSize);

            return View(model);
        }

        #region Resultats
        public async Task<ActionResult> Resultats(int id)
        {
            #region Check Controle id & find it
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var controle = await context.DemandeAccesEngin.FindAsync(id);
            if (controle == null)
            {
                return HttpNotFound();
            }

            #endregion

            #region get & check checklist
            var checkList = await context.TypeCheckList.Where(x => x.Id == controle.TypeCheckListId).FirstOrDefaultAsync();
            if (checkList == null)
            {
                return HttpNotFound();
            }
            #endregion

            #region get & check resultat entete 
            var resultatEntete = await context.DemandeResultatEntete.Where(x => x.DemandeAccesEnginId == controle.Id).FirstOrDefaultAsync();
            if (resultatEntete == null)
            {
                return null;
            }
            #endregion

            #region get & Type Engin
            var typeEngin = await context.TypeEngin.Where(x => x.Id == controle.TypeEnginId).FirstOrDefaultAsync();

            if (typeEngin == null)
            {
                return HttpNotFound();
            }
            #endregion

            #region get & Nature de la Matiere
            var natureMatiere = await context.NatureMatiere.Where(x => x.Id == controle.NatureMatiereId).FirstOrDefaultAsync();

            #endregion

            #region Conformité
            var resultatExigenceDetail = resultatEntete.ResultatExigence.ToList();
            var exigences = resultatEntete.ResultatExigence.ToList();
            var exigenceNonApplicable = resultatExigenceDetail.Where(x => !x.IsConform).ToList();
            var exigencesNonApplicableCount = exigenceNonApplicable.LongCount();
            var exigenceApplicable = resultatExigenceDetail.Where(x => x.IsConform).ToList();
            var exigencesApplicableCount = exigenceApplicable.LongCount();

            var Total = (exigencesApplicableCount + exigencesNonApplicableCount);

            var exigencesApplicable = (exigencesApplicableCount * 100) / Total;
            var exigencesNonApplicable = (exigencesNonApplicableCount * 100) / Total;

            #endregion

            #region Resultats Model
            //To Model
            var ResultatViewModel = new ResultatsVM
            {
                TypeCheckList = checkList,
                controle = controle,
                DemandeResultat = resultatEntete,
                TypeEngin = typeEngin,
                NatureMatiere = natureMatiere,
                exigencesApplicable = exigencesApplicable,
                exigencesNonApplicable = exigencesNonApplicable
            };

            #endregion


            return View(ResultatViewModel);
        }

        public async Task<ActionResult> NewControleResultatCheckList(int id)
        {


            #region Check Controle id & find it
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var controle = await context.DemandeAccesEngin.FindAsync(id);
            if (controle == null)
            {
                return HttpNotFound();
            }

            #endregion

            #region get & check checklist
            var checkList = await context.TypeCheckList.Where(x => x.Id == controle.TypeCheckListId).FirstOrDefaultAsync();

            if (checkList == null)
            {
                return HttpNotFound();
            }
            #endregion


            #region Model New Controle

            var controleCheckListVM = new ResultatsVM
            {
                TypeCheckList = checkList,
                controle = controle,
                TypeEngin = controle.TypeEngin,
                NatureMatiere = controle.NatureMatiere,
            };

            #endregion

            return View(controleCheckListVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewControleResultatCheckList(ResultatsVM ResultatExigence, HttpPostedFileBase file)
        {
            //ResultatsVM importData = SessionBag.Current.ImportData;
            int line = 0;
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    long fileId = 0;
                    var biz = new CommonBiz(context, log);

                    #region Save entete resultat Exigence 
                    var resultatEntete = new DemandeResultatEntete()
                    {
                        DemandeAccesEnginId = ResultatExigence.DemandeAccesEnginId,
                        CreatedBy = CurrentUserId,
                        CreatedOn = DateTime.Now,
                    };
                    #endregion

                    context.DemandeResultatEntete.Add(resultatEntete);

                    #region case row has file
                    if (file != null)
                    {
                        //add file to database
                        fileId = await biz.SaveOCPFile(file, ContainerName, resultatEntete.Id, SourceName);

                        //verify if file was added
                        if (fileId == 0)
                        {
                            return HttpNotFound();
                        }
                        resultatEntete.AppFileId = fileId;
                    }
                    #endregion


                    #region GET CHECKLISTEXIIGENCE ISHASDATE
                    var listExigenceHasDate = context.CheckListExigence.Where(x => x.IsHasDate).Select(c => c.Id).ToList();
                    #endregion


                    #region Save resultat Exigence
                    foreach (var resultatEx in ResultatExigence.ResultatExigenceList)
                    {
                        line++;
                        var controlResultatExigence = new ResultatExigence()
                        {
                            DemandeResultatEnteteId = resultatEntete.Id,
                            CheckListExigenceId = resultatEx.CheckListExigenceId,
                            IsConform = resultatEx.IsConform,
                            Observation = resultatEx.Observation,
                        };

                        if (listExigenceHasDate.Contains(resultatEx.CheckListExigenceId))
                        {
                            if (!string.IsNullOrEmpty(resultatEx.Date))
                            {
                                controlResultatExigence.Date = string.IsNullOrEmpty(resultatEx.Date) ? (DateTime?)null : Convert.ToDateTime(resultatEx.Date);

                            }
                            else
                            {
                                TempData[ConstsAccesEngin.MESSAGE_ERROR] = "s'il vous plaît vérifier les dates obligatoire! " + line;


                                return RedirectToAction("NewControleResultatCheckList", "Home", new { @id = ResultatExigence.DemandeAccesEnginId });

                            }
                        }
                        else
                        {
                            controlResultatExigence.Date = string.IsNullOrEmpty(resultatEx.Date) ? (DateTime?)null : Convert.ToDateTime(resultatEx.Date);
                        }

                        context.ResultatExigence.Add(controlResultatExigence);

                    }
                    #endregion

                    #region Get Demande acces 
                    DemandeAccesEngin demandeAccesEngin = await context.DemandeAccesEngin.FindAsync(ResultatExigence.DemandeAccesEnginId);
                    demandeAccesEngin.Autorise = ResultatExigence.Autorise;
                    #endregion

                    await context.SaveChangesAsync();





                    #region Send Mail To Chef project

                    var Email = demandeAccesEngin.AspNetUsers.Email;
                    var Subject = "contôle de " + demandeAccesEngin.TypeCheckList.Name;
                    //   var lettre = $@"";
                    var lettre = "<div><div><i><br></i></div><div><i>Bonjour M/Mme " + Email + "<br></i></div><div><i>Votre demande réferencée " + demandeAccesEngin.Id + " a été traité. </i></div><div><i>Votre engin est " + (demandeAccesEngin.Autorise ? "autorisé" : "refusé") + ". </i></div><div><i>Pour plus de détail veuillez consulter le lien suivant...... : http://ocpaccesengins.azurewebsites.net/Home/Resultats/" + demandeAccesEngin.Id + " </i></div><div><i> Bien cordialement</i></div><div><span style=\"color:rgb(32,37,42);font-family:Roboto,RobotoDraft,Helvetica,Arial,sans-serif;font-size:14px;font-weight:700\">L'équipe prévention HSE du site est à votre disposition pour toute information complémentaire</span><br></div></div>";
                    await MailHelper.SendEmailGHSE(new List<string> { "elmehdielmellali.mobile@gmail.com" }, lettre, Subject);
                    #endregion


                    transaction.Commit();
                    return RedirectToAction("Index", "DemandeAccesEngins");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Debug.WriteLine(ex.Message);
                    TempData[ConstsAccesEngin.MESSAGE_ERROR] = "s'il vous plaît vérifier les dates obligatoire!";
                    return View();
                }
            }
        }
        #endregion

        [ActionName("DownloadExcel")]
        public async Task<ActionResult> DownloadExcelAsync(int? id)
        {
            var cc = new DownloadResultatsToExcel(context, log);
            var toto = await cc.GetResultatToExcelAsync(id); ;

            if (toto != null)
            {
                byte[] filecontent = toto;
                return File(filecontent, cc.ExcelContentType, $"Resultats.xlsx");
            }
            else
            {
                TempData[ConstsAccesEngin.MESSAGE_ERROR] = "Erreur telechargement fichier Excel.";
                return RedirectToAction("Index");
            }

        }

        public async Task<ActionResult> PrintResultatsViewToPdf(int id)
        {


            #region Check Controle id & find it
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var controle = await context.DemandeAccesEngin.FindAsync(id);
            if (controle == null)
            {
                return HttpNotFound();
            }

            #endregion


            #region get & check resultat entete 
            var resultatEntete = await context.DemandeResultatEntete.Where(x => x.DemandeAccesEnginId == controle.Id).FirstOrDefaultAsync();
            if (resultatEntete == null)
            {
                return null;
            }
            #endregion


            #region get & check checklist
            var checkList = await context.TypeCheckList.Where(x => x.Id == controle.TypeCheckListId).FirstOrDefaultAsync();
            if (checkList == null)
            {
                return HttpNotFound();
            }
            #endregion

            #region get & Type Engin
            var typeEngin = await context.TypeEngin.Where(x => x.Id == controle.TypeEnginId).FirstOrDefaultAsync();

            if (typeEngin == null)
            {
                return HttpNotFound();
            }
            #endregion

            #region Conformité
            var resultatExigenceDetail = resultatEntete.ResultatExigence.ToList();
            var exigences = resultatEntete.ResultatExigence.ToList();
            var exigenceNonApplicable = resultatExigenceDetail.Where(x => !x.IsConform).ToList();
            var exigencesNonApplicableCount = exigenceNonApplicable.LongCount();
            var exigenceApplicable = resultatExigenceDetail.Where(x => x.IsConform).ToList();
            var exigencesApplicableCount = exigenceApplicable.LongCount();

            var Total = (exigencesApplicableCount + exigencesNonApplicableCount);

            var exigencesApplicable = (exigencesApplicableCount * 100) / Total;
            var exigencesNonApplicable = (exigencesNonApplicableCount * 100) / Total;

            #endregion

            #region Resultats Model
            //To Model
            var ResultatViewModel = new ResultatsVM
            {
                TypeCheckList = checkList,
                controle = controle,
                DemandeResultat = resultatEntete,
                TypeEngin = typeEngin,
                exigencesApplicable = exigencesApplicable,
                exigencesNonApplicable = exigencesNonApplicable
            };

            #endregion






            var syntese = new PartialViewAsPdf("/Views/Shared/ResultatsPDF.cshtml", ResultatViewModel)
            {
                PageSize = Rotativa.Options.Size.A3
            };
            return syntese;


        }

        #region GET FILE FROM AZURE

        public async Task<ActionResult> GetFileAzure(long? id)
        {
            var biz = new CommonBiz(context, log);

            var file = await context.AppFile.FindAsync(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            // var filePath = file.SystemFileName;
            //  var tt = biz.GetBlobBytes(file.SystemFileName,ContainerName);
            byte[] fileBytes = await biz.GetBlobBytes(file.SystemFileName, ContainerName);
            return File(fileBytes, MimeMapping.GetMimeMapping(file.OriginalFileName), Path.GetFileName(file.OriginalFileName));
        }

        #endregion


    }
}