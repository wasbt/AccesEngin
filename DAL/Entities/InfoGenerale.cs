namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InfoGenerale")]
    public partial class InfoGenerale
    {
        public InfoGenerale()
        {
            TypeCheckList = new HashSet<TypeCheckList>();
            ResultatInfoGenerale = new HashSet<ResultatInfoGenerale>();
        }
        public long Id { get; set; }

        public long InfoGeneralRubriqueId { get; set; }

        [Required]
        public string Name { get; set; }
        
        public DateTime CreatedOn { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual InfoGeneralRubrique InfoGeneralRubrique { get; set; }

        public virtual ICollection<TypeCheckList> TypeCheckList { get; set; }

        public virtual ICollection<ResultatInfoGenerale> ResultatInfoGenerale { get; set; }

    }
}
