namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TypeCheckList")]
    public partial class TypeCheckList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TypeCheckList()
        {
            CheckListRubrique = new HashSet<CheckListRubrique>();
            TypeEngin = new HashSet<TypeEngin>();
            DemandeAccesEngin = new HashSet<DemandeAccesEngin>();
            InfoGenerale = new HashSet<InfoGenerale>();
            NatureMatiere = new HashSet<NatureMatiere>();
        }

        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CheckListRubrique> CheckListRubrique { get; set; }
        public virtual ICollection<TypeEngin> TypeEngin { get; set; }
        public virtual ICollection<NatureMatiere> NatureMatiere { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DemandeAccesEngin> DemandeAccesEngin { get; set; }

        public virtual ICollection<InfoGenerale> InfoGenerale { get; set; }

    }
}
