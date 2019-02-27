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
    public class SitesEntitiesBiz : CommonBiz
    {
        public SitesEntitiesBiz(EnginDbContext context, ILog log) : base(context, log)
        {

        }

        public List<EntityDTO> GetEntityBySite(GetEntityBySiteModel model)
        {
            var entities = context.Entity.Where(i => i.SiteId == model.SiteId).ToList();

            var entitiesDto = entities.Select(te => te.EntityToDTO()).ToList();

            return entitiesDto;
        }
    }
}
