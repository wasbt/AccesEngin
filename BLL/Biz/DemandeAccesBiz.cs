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
using System.Collections.ObjectModel;
using Shared;

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

            var typeCheckList = await context.TypeCheckList.Where(x => x.Id == id).FirstOrDefaultAsync();

            var typeCheckListDTO = typeCheckList.TypeCheckListToDTO();

            var tt = typeCheckListDTO.Rubriques.GroupBy(r => r.Name).Select(x => new Grouping<string, CheckListRubriqueDTO>(x.Key, x)).ToList();
            #endregion
            typeCheckListDTO.RubriquesGrouping = tt;

            return typeCheckListDTO;
        }



    }
    
}
