using BLL.Common;
using DAL;
using log4net;
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
            var CountController = context.DemandeAccesEngin.Where(x => x.CreatedBy == CurrentUser && x.DemandeResultatEntete.Any()).LongCount();
            var CountNonController = context.DemandeAccesEngin.Where(x => x.CreatedBy == CurrentUser && !x.DemandeResultatEntete.Any()).LongCount();
            var result = new KpiModel()
            {
                CountController = CountController,
                CountNonController = CountNonController,

            };
            return result;
        }

        public async Task<KpiModel> MesDemandeAutorise(string CurrentUser)
        {
            var CountAutorise = context.DemandeAccesEngin.Where(x => x.CreatedBy == CurrentUser && x.DemandeResultatEntete.Any() && x.Autorise).LongCount();
            var CountNonAutorise = context.DemandeAccesEngin.Where(x => x.CreatedBy == CurrentUser && x.DemandeResultatEntete.Any() && !x.Autorise).LongCount();
            var result = new KpiModel()
            {
                CountController = CountAutorise,
                CountNonController = CountNonAutorise,

            };
            return result;
        }
        #endregion

        #region KPIS FOR ROLE CONTROLLEUR


        public async Task<KpiModel> DemandeAutoriseByControlleur(string CurrentUser)
        {
            var CountAutorise = context.DemandeAccesEngin.Where(x => x.DemandeResultatEntete.Any(r => r.CreatedBy == CurrentUser) && x.Autorise).LongCount();
            var CountNonAutorise = context.DemandeAccesEngin.Where(x => x.DemandeResultatEntete.Any(r => r.CreatedBy == CurrentUser) && !x.Autorise).LongCount();
            var result = new KpiModel()
            {
                CountController = CountAutorise,
                CountNonController = CountNonAutorise,

            };
            return result;
        }
        #endregion


    }
}
