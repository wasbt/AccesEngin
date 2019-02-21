namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InfoGeneralRubrique")]
    public partial class InfoGeneralRubrique
    {
        public InfoGeneralRubrique()
        {
            InfoGenerale = new HashSet<InfoGenerale>();
        }

        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int ShowOrder { get; set; }

        public bool IsActif { get; set; }

        public DateTime CreatedOn { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual ICollection<InfoGenerale> InfoGenerale { get; set; }

    }
}
