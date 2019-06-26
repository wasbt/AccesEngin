using DAL;
using Front.Core.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Front.Persistance.Repositories
{
    public class TypeCheckListsRepository : ITypeCheckListsRepository
    {
        private readonly IOcpPerformanceDataContext _context;

        public TypeCheckListsRepository(IOcpPerformanceDataContext context)
        {
            _context = context;
        }

        public REF_TypeCheckList Add(REF_TypeCheckList typeCheckList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<REF_TypeCheckList> GetAllTypeCheckList()
        {
            //return _context.REF_TypeCheckList.Select(x=>x.TypeCheckListToDTO());
            return _context.REF_TypeCheckList;
        }



    }
}
