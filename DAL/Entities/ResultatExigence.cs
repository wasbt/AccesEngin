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

        public long DemandeResultatEnteteId { get; set; }

        public long CheckListExigenceId { get; set; }

        public bool IsConform { get; set; }

        public System.Nullable<DateTime> Date { get; set; }

        public string Observation { get; set; }

        public virtual CheckListExigence CheckListExigence { get; set; }

        public virtual DemandeResultatEntete DemandeResultatEntete { get; set; }

    }
}
