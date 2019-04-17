using BLL.Common;
using DAL;
using log4net;
using Shared.ENUMS;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Biz
{
    public class KpiBiz : CommonBiz
    {
        public KpiBiz(EnginDbContext context, ILog log) : base(context, log)
        {
        }

        #region KPIS FOR ROLE CHEF PROJET
        public async Task<KpiModel> MesDemande(string CurrentUser)
        {
            var CountController = context.DemandeAccesEngin.Where(x =>
            x.StatutDemandeId != (int)DemandeStatus.Expirer &&
            x.CreatedBy == CurrentUser &&
            x.DemandeResultatEntete.Any()).LongCount();

            var CountNonController = context.DemandeAccesEngin.Where(x =>
             x.StatutDemandeId != (int)DemandeStatus.Expirer &&
            x.CreatedBy == CurrentUser &&
            !x.DemandeResultatEntete.Any()).LongCount();
            var result = new KpiModel()
            {
                Value1 = CountController,
                Value2 = CountNonController,

            };
            return result;
        }

        public async Task<KpiModel> MesDemandeAutorise(string CurrentUser)
        {
            var CountAutorise = context.DemandeAccesEngin.Where(x =>
              x.StatutDemandeId != (int)DemandeStatus.Expirer &&
            x.CreatedBy == CurrentUser &&
            x.DemandeResultatEntete.Any() && x.Autorise).LongCount();

            var CountNonAutorise = context.DemandeAccesEngin.Where(x =>
                  x.StatutDemandeId != (int)DemandeStatus.Expirer &&
            x.CreatedBy == CurrentUser &&
            x.DemandeResultatEntete.Any() && !x.Autorise).LongCount();

            var result = new KpiModel()
            {
                Value1 = CountAutorise,
                Value2 = CountNonAutorise,

            };
            return result;
        }
        #endregion

        #region KPIS FOR ROLE CONTROLLEUR


        public async Task<KpiModel> DemandeAutoriseByControlleur(string CurrentUser)
        {
            var CountAutorise = context.DemandeAccesEngin.Where(x =>
                          x.StatutDemandeId == (int)DemandeStatus.Expirer &&
            x.DemandeResultatEntete.Any(r => r.CreatedBy == CurrentUser) &&
            x.Autorise).LongCount();


            var CountNonAutorise = context.DemandeAccesEngin.Where(x =>
                                      x.StatutDemandeId == (int)DemandeStatus.Expirer &&
            x.DemandeResultatEntete.Any(r => r.CreatedBy == CurrentUser) &&
            !x.Autorise).LongCount();

            var result = new KpiModel()
            {
                Value1 = CountAutorise,
                Value2 = CountNonAutorise,

            };
            return result;
        }
        #endregion

        public async Task<KpiModel> MesDemandeExpire(string CurrentUser = null)
        {
            var QueryCountExpire = context.DemandeAccesEngin.AsQueryable();
            var QueryCountNonExpire = context.DemandeAccesEngin.AsQueryable();

            if (!string.IsNullOrWhiteSpace(CurrentUser))
            {
                QueryCountExpire = QueryCountExpire.Where(x => x.CreatedBy == CurrentUser);
                QueryCountNonExpire = QueryCountNonExpire.Where(x => x.CreatedBy == CurrentUser);

            }

            var CountExpire = QueryCountExpire.Where(x =>
              x.StatutDemandeId == (int)DemandeStatus.Expirer &&
              x.DemandeResultatEntete.Any()).LongCount();

            var CountSorti =  QueryCountNonExpire.Where(x =>
             x.StatutDemandeId == (int)DemandeStatus.Sortir &&
            x.DemandeResultatEntete.Any()).LongCount();

            var CountAccepte =  QueryCountNonExpire.Where(x =>
             x.StatutDemandeId == (int)DemandeStatus.Accepter &&
            x.DemandeResultatEntete.Any()).LongCount();


            var CountRefuse =  QueryCountNonExpire.Where(x =>
             x.StatutDemandeId == (int)DemandeStatus.Refuser &&
            x.DemandeResultatEntete.Any()).LongCount();

            var result = new KpiModel()
            {
                Value1 = CountExpire,
                Value2 = CountSorti,
                Value3 = CountAccepte,
                Value4 = CountRefuse,

            };
            return result;
        }

    }
}
