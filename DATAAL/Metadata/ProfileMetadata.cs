using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATAAL
{
    public partial class ProfileMetadata
    {
        [Display(Name = "#")]
        public string Id { get; set; }
        [Display(Name = "Nom")]
        public string FullName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Téléphone")]
        public string Phone { get; set; }
        [Display(Name = "Description")]
        public string Details { get; set; }
        [Display(Name = "Date de dernière connexion")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DtLastConnection { get; set; }
        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}
