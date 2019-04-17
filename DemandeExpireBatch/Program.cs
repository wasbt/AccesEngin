using DAL;
using log4net;
using Shared;
using Shared.ENUMS;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemandeExpireBatch
{
    class Program
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static EnginDbContext context;
        static async Task Main(string[] args)
        {
            using (context = new EnginDbContext())
            {
                try
                {
                    #region init logger
                    log4net.Config.XmlConfigurator.Configure();
                    #endregion

                    var demandes = context
                    .DemandeAccesEngin
                    .Where(x =>
                    x.Autorise &&
                    x.StatutDemandeId != (int)DemandeStatus.Sortir &&
                    x.StatutDemandeId != (int)DemandeStatus.Expirer &&
                    x.DemandeResultatEntete.Any(y => y.ResultatExigence.Any(d => d.Date.HasValue && DbFunctions.DiffDays(DateTime.Now, d.Date.Value) <= 15 && DbFunctions.DiffDays(DateTime.Now, d.Date.Value) >= 0)));


                    foreach (var item in demandes)
                    {
                        var emails = context.REF_MailingList.Where(x => x.EntityId == item.EntityId).Select(m => m.AspNetUsers.Select(e => e.Email)).ToList();
                        #region Send Mail To Chef project

                        var Email = item.AspNetUsers.Email;
                        var Subject = "contôle de " + item.TypeCheckList.Name;
                        //   var lettre = $@"";
                        var lettre = "<div><div><i><br></i></div><div><i>Bonjour M/Mme " + Email + "<br></i></div><div><i>Votre demande réferencée " + item.Id + " a été traité. </i></div><div><i>Pour plus de détail veuillez consulter le lien suivant...... : http://ocpaccesengins.azurewebsites.net/Home/Resultats/" + item.Id + " </i></div><div><i> Bien cordialement</i></div><div><span style=\"color:rgb(32,37,42);font-family:Roboto,RobotoDraft,Helvetica,Arial,sans-serif;font-size:14px;font-weight:700\">L'équipe prévention HSE du site est à votre disposition pour toute information complémentaire</span><br></div></div>";
                        await MailHelper.SendEmailDemandeEngin(new List<string> { "elmehdielmellali.mobile@gmail.com" }, lettre, Subject);
                        #endregion
                    }


                    var demandesExpire = context
                       .DemandeAccesEngin
                       .Where(x =>
                       x.Autorise &&
                       x.StatutDemandeId != (int)DemandeStatus.Sortir &&
                       x.StatutDemandeId != (int)DemandeStatus.Expirer &&
                       x.DemandeResultatEntete.Any(y => y.ResultatExigence.Any(d => d.Date.HasValue && DbFunctions.DiffDays(DateTime.Now, d.Date.Value) <= 0)));


                    foreach (var item in demandesExpire)
                    {
                        item.StatutDemandeId = (int)DemandeStatus.Expirer;
                        context.Entry(item).State = EntityState.Modified;
                    }
                    await context.SaveChangesAsync();

                }
                catch (Exception Ex)
                {
                    log.Error(Ex.Message + ": " + Ex);
                }
            }
        }

    }
}
