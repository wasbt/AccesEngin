using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial class Entity
    {
        public Entity()
        {
            DemandeAccesEngin = new HashSet<DemandeAccesEngin>();
            Profiles = new HashSet<Profile>();
            REF_MailingList = new HashSet<REF_MailingList>();

        }
        public long Id { get; set; }

        public long SiteId { get; set; }

        public string HSEEntiteUserId { get; set; }

        public string ADFUserId { get; set; }

        public string ResponsableEntiteUserId { get; set; }

        public string Name { get; set; }

        public bool? IsHSE { get; set; }

        public System.DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual Profile Profile { get; set; }

        public virtual Site Site { get; set; }

        public virtual ICollection<DemandeAccesEngin> DemandeAccesEngin { get; set; }
        public virtual ICollection<Profile> Profiles { get; set; }

        public virtual ICollection<REF_MailingList> REF_MailingList { get; set; }

    }
}
