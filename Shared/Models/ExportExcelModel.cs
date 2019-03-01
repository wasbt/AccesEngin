using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class ExportExcelModel
    {
        [DisplayName("Entité")]
        public string Entity { get; set; }

        [DisplayName("Chef de projet")]
        public string ChefProjet { get; set; }

        [DisplayName("N° Commande")]
        public string NCommande { get; set; }

        [DisplayName("Entreprise Propriétaire")]
        public string EntrepriseProprietaire { get; set; }

        [DisplayName(" Entreprise Utilisatrice")]
        public string EntrepriseUtilisatrice { get; set; }

        [DisplayName("Type d'engin")]
        public string TypeEngin { get; set; }

        [DisplayName("Châssis")]
        public string Chassis { get; set; }

        [DisplayName("Matricule")]
        public string Matricule { get; set; }

        [DisplayName("Constructeur")]
        public string Constructeur { get; set; }

        [DisplayName("Matricule de la citerne")]
        public string MatriculeCiterne { get; set; }

        [DisplayName("Nature de la matière")]
        public string NatureMtiere { get; set; }

        [DisplayName("Date fin Contrôle Rég")]
        public string DateFinControleReg { get; set; }

        [DisplayName("Date Fin Assurance")]
        public string DateFinAssurance { get; set; }

        [DisplayName("Nom Conducteur")]
        public string NomConducteur { get; set; }

        [DisplayName("CIN Conducteur")]
        public string CINConducteur { get; set; }

        [DisplayName("Permis de conduire")]
        public string PermisConduire { get; set; }

        [DisplayName("Date")]
        public string Date { get; set; }

        [DisplayName("Contrôleur")]
        public string Controleur { get; set; }

        [DisplayName("Autorisé")]
        public string Autorise { get; set; }

        [DisplayName("Observation")]
        public string Observation { get; set; }

        [DisplayName("Date de validité de permis")]
        public string DateValiditePermis { get; set; }
    }
}
