namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TypeEngin")]
    public partial class TypeEngin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TypeEngin()
        {
            DemandeAccesEngin = new HashSet<DemandeAccesEngin>();
        }

        public long Id { get; set; }

        public long TypeCheckListId { get; set; }

        [Required]
        public string Name { get; set; }

        public string DureeEstimative { get; set; }

        public DateTime CreatedOn { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; } 

        public virtual TypeCheckList TypeCheckList { get; set; }

        public virtual ICollection<DemandeAccesEngin> DemandeAccesEngin { get; set; }
    }
}
