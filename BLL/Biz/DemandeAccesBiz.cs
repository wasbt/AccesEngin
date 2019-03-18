using BLL.Common;
using DAL;
using log4net;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Biz
{
    public class DemandeAccesBiz : CommonBiz
    {
        public DemandeAccesBiz(EnginDbContext context, ILog log) : base(context, log)
        {
        }

        public List<DemandeAccesDto> DemandeAccesList()
        {
            var demandeAccesList =  context.DemandeAccesEngin.ToList();
            return demandeAccesList.Select(x => x.DemandeAccesToDTO()).ToList();
        }

    }
}
