using Shared.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATAAL
{
    [MetadataType(typeof(REF_CheckListRubriqueMetadata))]
    public partial class REF_CheckListRubrique
    {
        public CheckListRubriqueDTO CheckListRubriqueToDTO()
        {
            var model = this;
            var dto = new CheckListRubriqueDTO()
            {
                Id = model.Id,
                Name = model.Name,
                Exigences = model.REF_CheckListExigence.Select(x => x.CheckListExigenceToDTO()).ToList(),
                IsActif = model.IsActif,
                ShowOrder = model.ShowOrder,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
            };
            return dto;
        }
    }
}
