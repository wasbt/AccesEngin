//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ResultatControleDetail
    {
        public long Id { get; set; }
        public long ResultatControleEnteteId { get; set; }
        public long CheckListExigenceId { get; set; }
        public bool IsConform { get; set; }
        public Nullable<System.DateTime> DateExpiration { get; set; }
        public string Observation { get; set; }
    
        public virtual REF_CheckListExigence REF_CheckListExigence { get; set; }
        public virtual ResultatControleEntete ResultatControleEntete { get; set; }
    }
}
