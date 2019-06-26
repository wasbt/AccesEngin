using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Front.Models
{
    public class StandardModel<T>
    {
        public int? id { get; set; }
        public string columnName { get; set; }
        public string content { get; set; }
        public int? page { get; set; }
        public int? newSearch { get; set; }

        public IPagedList<T> resultList { get; set; }
        public IEnumerable<T> simpleList { get; set; }
    }
}
