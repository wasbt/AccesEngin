using Shared.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [MetadataType(typeof(EntitiesMetadata))]
    public partial class Entite
    {
        public EntityDTO EntityToDTO()
        {
            var model = this;
            var dto = new EntityDTO()
            {
              Id = model.Id,
              Name = model.Name,
              SiteId = model.SiteId
            };
            return dto;
        }
    }
}
