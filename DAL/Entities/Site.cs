using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial class Site
    {
        public Site()
        {
            Entity = new HashSet<Entity>();

        }

        public long Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber1 { get; set; }

        public string PhoneNumber2 { get; set; }

        public DateTime CreatedOn { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual ICollection<Entity> Entity { get; set; }



    }
}
