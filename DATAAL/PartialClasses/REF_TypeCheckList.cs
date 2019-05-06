using Shared.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATAAL
{
    [MetadataType(typeof(REF_TypeCheckListMetadata))]

    public partial class REF_TypeCheckList
    {
        public TypeCheckListDTO TypeCheckListToDTO()
        {
            var model = this;
            var dto = new TypeCheckListDTO()
            {
                Id = model.Id,
                Name = model.Name,
                Rubriques = model.REF_CheckListRubrique.Select(x => x.CheckListRubriqueToDTO()).ToList(),
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
            };
            return dto;
        }
    }
}
