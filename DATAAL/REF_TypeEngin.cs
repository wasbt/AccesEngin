//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DATAAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class REF_TypeEngin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public REF_TypeEngin()
        {
            this.DemandeAccesEngin = new HashSet<DemandeAccesEngin>();
        }
    
        public long Id { get; set; }
        public long TypeCheckListId { get; set; }
        public string Name { get; set; }
        public string DureeEstimative { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DemandeAccesEngin> DemandeAccesEngin { get; set; }
        public virtual REF_TypeCheckList REF_TypeCheckList { get; set; }
    }
}
