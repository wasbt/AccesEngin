using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Front.Core.Repositories
{
    public interface ITypeCheckListsRepository
    {
        IEnumerable<REF_TypeCheckList> GetAllTypeCheckList();
        REF_TypeCheckList Add(REF_TypeCheckList typeCheckList);
    }
}
