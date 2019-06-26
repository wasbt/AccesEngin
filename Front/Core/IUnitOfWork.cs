using Front.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Front.Core
{
    public interface IUnitOfWork
    {
         ITypeCheckListsRepository TypeCheckLists { get; set; }
        void Commit();
    }
}
