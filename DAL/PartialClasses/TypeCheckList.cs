using Shared.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [MetadataType(typeof(TypeCheckListMetadata))]

    public partial class TypeCheckList
    {
        public TypeCheckListDTO TypeCheckListToDTO()
        {
            var model = this;
            var dto = new TypeCheckListDTO()
            {
                Id = model.Id,
                Name = model.Name,
                Rubriques = model.CheckListRubrique.Select(x => x.CheckListRubriqueToDTO()).ToList(),
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
            };
            return dto;
        }
    }
}
