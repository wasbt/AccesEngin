using Shared.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATAAL
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
                TypeCheckListId = model.TypeCheckListId,
                TypeEnginName = model.REF_TypeEngin.Name,
                TypeCheckListName = model.REF_TypeCheckList.Name,
                NatureMatiereName = model?.REF_NatureMatiere?.Name,
                EntityName = model.Entities.Name,
                DatePlannification = model.DatePlannification,
                Autorise = model.Autorise,
                Observation = model?.Observation,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
                CreatedEmail = model.AspNetUsers.Email,
                AutoriseName = model.Autorise ? "Autorisé" : "Non autorisé",
                Statut = model?.REF_StatutDemandes?.Name,
                StatutColor = model?.REF_StatutDemandes?.Color,


            };
            return dto;
        }

    }
}
