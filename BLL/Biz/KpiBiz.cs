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


        public async Task<KpiModel> MesDemande(string CurrentUser)
        {
            var CountController = context.DemandeAccesEngin.Where(x => x.CreatedBy == CurrentUser && x.DemandeResultatEntete.Any()).LongCount();
            var CountNonController = context.DemandeAccesEngin.Where(x => x.CreatedBy == CurrentUser &&  !x.DemandeResultatEntete.Any()).LongCount();
           var result = new KpiModel()
            {
                CountController = CountController,
                CountNonController = CountNonController,

            };
            return result;
        }
    }
}
