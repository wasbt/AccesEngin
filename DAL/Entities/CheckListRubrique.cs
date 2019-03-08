namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CheckListRubrique")]
    public partial class CheckListRubrique
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CheckListRubrique()
        {
            CheckListExigence = new HashSet<CheckListExigence>();
        }

        public long Id { get; set; }

        public long TypeCheckListId { get; set; }

        [Required]
        public string Name { get; set; }

        public int ShowOrder { get; set; }

        public bool IsActif { get; set; }

        public DateTime CreatedOn { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CheckListExigence> CheckListExigence { get; set; }

        public virtual TypeCheckList TypeCheckList { get; set; }
    }
}
