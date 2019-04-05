namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Profile")]
    public partial class Profile
    {
        public Profile()
        {
            Site = new HashSet<Site>();
            Entities = new HashSet<Entity>();
        }
        public string Id { get; set; }
        public long? EntiteId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Details { get; set; }

        public DateTime? DtLastConnection { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual ICollection<Site> Site { get; set; }

        public virtual ICollection<Entity> Entities { get; set; }

        public virtual Entity Entity { get; set; }
    }
}
