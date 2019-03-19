using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial class DemandeResultatEntete
    {
        public DemandeResultatEnteteDTO DemandeResultatEnteteToDTO()
        {
            var model = this;
            var dto = new DemandeResultatEnteteDTO()
            {
                Id = model.Id,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
            };
            return dto;
        }
    }
}
