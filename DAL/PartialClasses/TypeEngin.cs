using Shared.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [MetadataType(typeof(TypeEnginMetadata))]
    public partial class TypeEngin
    {
        public TypeEnginDTO TypeEnginToDTO()
        {
            var model = this;
            var dto = new TypeEnginDTO()
            {
                Id = model.Id,
                TypeCheckListId = model.TypeCheckListId,
                Name = model.Name,
                DureeEstimative = model.DureeEstimative,
                CreatedBy = model.CreatedBy,
                CreatedOn =  model.CreatedOn
            };
            return dto;
        }
    }
}
