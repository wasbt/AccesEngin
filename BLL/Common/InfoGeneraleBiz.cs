using DAL;
using log4net;
using Shared.API.IN;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Common
{
    public class InfoGeneraleBiz : CommonBiz
    {
        public InfoGeneraleBiz(EnginDbContext context, ILog log) : base(context, log)
        {
        }

        public IList<InfoGeneraleDTO> GetInfoGeneralesByyTypeCheckList(GetInfoGeneraleByTypeCheckList generaleByTypeCheckList)
        {

            return null;
        }
    }
}
