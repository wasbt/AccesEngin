using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class REF_MailingList
    {
        public REF_MailingList()
        {
            AspNetUsers = new HashSet<AspNetUsers>();
        }

        public string Id { get; set; }

        public long EntityId { get; set; }

        [Required]
        public string Name { get; set; }


        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }

        public virtual Entity Entity { get; set; }

    }
}
