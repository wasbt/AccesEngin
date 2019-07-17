using BLL.Common;
using DAL;
using Shared;
using Shared.ENUMS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemandeExpireBatch
{
    static class Program
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static OcpPerformanceDataContext context;
        static async Task Main(string[] args)
        {
            using (context = new OcpPerformanceDataContext())
            {
                try
                {
                    #region init logger
                    log4net.Config.XmlConfigurator.Configure();
                    #endregion

                    var demandes = context
                    .DemandeAccesEngin
                    .Where(x =>
                    x.IsAutorise &&
                    x.StatutDemandeId != (int)DemandeStatus.Sortir &&
                    x.StatutDemandeId != (int)DemandeStatus.Expirer &&
                    x.ResultatControleEntete.Any(y => y.ResultatControleDetail.Any(d => d.DateExpiration.HasValue && DbFunctions.DiffDays(DateTime.Now, d.DateExpiration.Value) <= 15 && DbFunctions.DiffDays(DateTime.Now, d.DateExpiration.Value) >= 0)));


                    foreach (var demande in demandes)
                    {
                        #region Send Mail To Chef project

                        var Email = demande.AspNetUsers.Email;
                        var DemandeurFullName = demande.AspNetUsers.Profile.FullName;
                        var ResultatControle = (demande.StatutDemandeId == (int)DemandeStatus.Accepter ? "<span style='font-weight:bold;color:Green'>Accepté.</span>" : "<span style='font-weight:bold;color:Red'>Refusé.</span>");
                        var Subject = $"Expiration demande pour: {demande.REF_TypeCheckList.Name}";
                        //   var lettre = $@"";
                        var lettre = $"Bonjour {DemandeurFullName},<br><br>"
                            + $"Votre demande réferencée {demande.Id} a été expirée.<br><br>"
                            + $"Votre engin est {ResultatControle} <br><br>"
                            + $"Pour plus de détails veuillez consulter le lien suivant : "
                            + $"<a href='https://myops.ocpgroup.ma/AccesEngins/AccesEnginsHome/Resultats/{demande.Id}'>Demande d'accès #{demande.Id}</a>" +
                            $"Bien cordialement<br><br>" +
                            $"<span style=\"color:rgb(32,37,42);font-family:Roboto,RobotoDraft,Helvetica,Arial,sans-serif;font-size:14px;font-weight:700\">" +
                            $"L'équipe prévention HSE du site est à votre disposition pour toute information complémentaire" +
                            $"</span>";

                        var CC_DigiControl_Jorf = ConfigurationManager.AppSettings["CC_DigiControl_Jorf"].Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
                        var BCC_DigiControl_Jorf = ConfigurationManager.AppSettings["BCC_DigiControl_Jorf"].Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

                        CommonBiz.SendEmail(new List<string> { Email },
                             Subject, lettre, null,
                            ccList: CC_DigiControl_Jorf,
                            BCCList: BCC_DigiControl_Jorf);
                        #endregion
                    }


                    var demandesExpire = context
                       .DemandeAccesEngin
                       .Where(x =>
                       x.IsAutorise &&
                       x.StatutDemandeId != (int)DemandeStatus.Sortir &&
                       x.StatutDemandeId != (int)DemandeStatus.Expirer &&
                       x.ResultatControleEntete.Any(y => y.ResultatControleDetail.Any(d => d.DateExpiration.HasValue && DbFunctions.DiffDays(DateTime.Now, d.DateExpiration.Value) <= 0)));


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
