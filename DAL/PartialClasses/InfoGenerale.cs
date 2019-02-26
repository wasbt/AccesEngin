using Shared.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [MetadataType(typeof(InfoGeneraleMetadata))]
    public partial class InfoGenerale
    {
        public InfoGeneraleDTO InfoGeneraleToDTO()
        {
            var model = this;
            var dto = new InfoGeneraleDTO()
            {
                Id = model.Id,
                InfoGeneralRubriqueId = model.InfoGeneralRubriqueId,
                Name = model.Name,
            };
            return dto;
        }
    }
}
