using Shared.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [MetadataType(typeof(REF_CheckListExigenceMetadata))]
    public partial class REF_CheckListExigence
    {
        public CheckListExigenceDTO CheckListExigenceToDTO()
        {
            var model = this;
            var dto = new CheckListExigenceDTO()
            {
                Id = model.Id,
                Name = model.Name,
                IsActif = model.IsActif,
                ShowOrder = model.ShowOrder,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
                IsHasDate = model.IsHasDate
            };
            return dto;
        }
    }
}
