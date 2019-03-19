using BLL.Common;
using DAL;
using log4net;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Biz
{
    public class DemandeAccesBiz : CommonBiz
    {
        public DemandeAccesBiz(EnginDbContext context, ILog log) : base(context, log)
        {
        }

        public List<DemandeAccesDto> DemandeAccesList()
        {
            var demandeAccesList =  context.DemandeAccesEngin.ToList();
            return demandeAccesList.Select(x => x.DemandeAccesToDTO()).ToList();
        }

        public async Task<TypeCheckListDTO> GetCheckListAsync(int id)
        {
            #region Check Controle id & find it

            var typeCheckList =  context.TypeCheckList.Where(x => x.Id == id).FirstOrDefault().TypeCheckListToDTO();

            var typeCheckListDTO = typeCheckList;

            #endregion


            return typeCheckListDTO;
        }



    }
}
