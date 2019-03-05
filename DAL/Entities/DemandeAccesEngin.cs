namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DemandeAccesEngin")]
    public partial class DemandeAccesEngin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DemandeAccesEngin()
        {
            ResultatExigence = new HashSet<ResultatExigence>();
            ResultatInfoGenerale = new HashSet<ResultatInfoGenerale>();
        }

        public long Id { get; set; }

        public int TypeCheckListId { get; set; }

        public long TypeEnginId { get; set; }

        public long EntityId { get; set; }

        [Required]
        public string Observation { get; set; }

        public bool? Autorise { get; set; }

        public DateTime DatePlannification { get; set; }

        public DateTime CreatedOn { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual TypeCheckList TypeCheckList { get; set; }

        public virtual TypeEngin TypeEngin { get; set; }

        public virtual Entity Entity { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResultatExigence> ResultatExigence { get; set; }
        public virtual ICollection<ResultatInfoGenerale> ResultatInfoGenerale { get; set; }
    }
}
