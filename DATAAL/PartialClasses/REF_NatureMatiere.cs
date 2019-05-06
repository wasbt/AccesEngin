using Shared.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATAAL
{
    [MetadataType(typeof(REF_NatureMatiereMetadata))]
    public partial class REF_NatureMatiere
    {
        public NatureMatiereDTO NatureMatiereToDTO()
        {
            var model = this;
            var dto = new NatureMatiereDTO()
            {
                Id = model.Id,
                TypeCheckListId = model.TypeCheckListId,
                Name = model.Name,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn
            };
            return dto;
        }
    }
}
