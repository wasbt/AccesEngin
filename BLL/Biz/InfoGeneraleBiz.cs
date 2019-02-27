using BLL.Common;
using DAL;
using log4net;
using Shared.API.IN;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Biz
{
    public class InfoGeneraleBiz : CommonBiz
    {
        public InfoGeneraleBiz(EnginDbContext context, ILog log) : base(context, log)
        {
        }

        public List<InfoGeneraleDTO> GetInfoGeneralesByTypeCheckList(GetInfoGeneraleByTypeCheckList generaleByTypeCheckList)
        {
            var infoGenerales = context.InfoGenerale.Where(i => i.TypeCheckList.Any(t => t.Id == generaleByTypeCheckList.TypeCheckListId)).ToList();

            var infoGeneralsDto = infoGenerales.Select(info => info.InfoGeneraleToDTO()).ToList();

            return infoGeneralsDto;
        }

        public List<TypeEnginDTO> GetTypeEnginByTypeCheckList(GetInfoGeneraleByTypeCheckList generaleByTypeCheckList)
        {
            var typeEngins = context.TypeEngin.Where(i => i.TypeCheckListId == generaleByTypeCheckList.TypeCheckListId).ToList();

            var TypeEnginDto = typeEngins.Select(te => te.TypeEnginToDTO()).ToList();

            return TypeEnginDto;
        }
    }
}
