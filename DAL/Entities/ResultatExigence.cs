namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ResultatExigence")]
    public partial class ResultatExigence
    {
        public long Id { get; set; }

        public long DemandeAccesEnginId { get; set; }

        public long CheckListExigenceId { get; set; }

        public bool IsConform { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string Oobservation { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual CheckListExigence CheckListExigence { get; set; }

        public virtual DemandeAccesEngin DemandeAccesEngin { get; set; }
    }
}
