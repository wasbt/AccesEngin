using BLL.Common;
using DAL;
using ExcelDataReader.Log;
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

        public List<int> MesDemande(string CurrentUser)
        {
            var model = context.DemandeAccesEngin.Where(x => x.CreatedBy == CurrentUser).Select(x => 
            new ModelKpi()
            {
                CountController = x

            };
            return null;
        }
    }
}
