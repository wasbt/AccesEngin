namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CheckListExigence")]
    public partial class CheckListExigence
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CheckListExigence()
        {
            ResultatExigence = new HashSet<ResultatExigence>();
        }

        public long Id { get; set; }

        public long CheckListRubriqueId { get; set; }

        [Required]
        public string Name { get; set; }

        public int ShowOrder { get; set; }

        public bool IsActif { get; set; }

        public bool IsHasDate { get; set; }

        public DateTime CreatedOn { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual CheckListRubrique CheckListRubrique { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResultatExigence> ResultatExigence { get; set; }
    }
}
