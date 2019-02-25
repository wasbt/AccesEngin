namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ResultatInfoGenerale")]
    public partial class ResultatInfoGenerale
    {
        public long Id { get; set; }

        public long DemandeAccesEnginId { get; set; }

        public long InfoGeneraleId { get; set; }

        [Required]
        public string ValueInfo { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual InfoGenerale InfoGenerale { get; set; }

        public virtual DemandeAccesEngin DemandeAccesEngin { get; set; }
    }
}
