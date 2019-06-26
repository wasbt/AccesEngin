using DAL;
using Front.Core;
using Front.Core.Repositories;
using Front.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Front.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OcpPerformanceDataContext _context;

        public ITypeCheckListsRepository TypeCheckLists { get; set ; }

        public UnitOfWork(OcpPerformanceDataContext context)
        {
            _context = context;
         //   TypeCheckLists = new TypeCheckListsRepository(_context);
        }
        public void Commit()
        {
            _context.SaveChangesAsync();
        }
    }
}
