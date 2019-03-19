using Shared.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [MetadataType(typeof(DemandeAccesEnginMetadata))]
    public partial class DemandeAccesEngin
    {
        public DemandeAccesDto DemandeAccesToDTO()
        {
            var model = this;
            var dto = new DemandeAccesDto()
            {
                Id = model.Id,
                TypeEnginName = model.TypeEngin.Name,
                TypeCheckListName = model.TypeCheckList.Name,
                NatureMatiereName = model.NatureMatiere.Name,
                EntityName = model.Entity.Name,
                DatePlannification = model.DatePlannification,
                Autorise = model.Autorise,
                Observation = model.Observation,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
            };
            return dto;
        }

    }
}
