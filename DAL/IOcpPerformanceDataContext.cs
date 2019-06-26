using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IOcpPerformanceDataContext
    {

         DbSet<DemandeAccesEngin> DemandeAccesEngin { get; set; }
         DbSet<DemandeAccesEnginInfoGeneraleValue> DemandeAccesEnginInfoGeneraleValue { get; set; }
         DbSet<REF_CheckListExigence> REF_CheckListExigence { get; set; }
         DbSet<REF_CheckListRubrique> REF_CheckListRubrique { get; set; }
         DbSet<REF_InfoGenerale> REF_InfoGenerale { get; set; }
         DbSet<REF_InfoGeneralRubrique> REF_InfoGeneralRubrique { get; set; }
         DbSet<REF_NatureMatiere> REF_NatureMatiere { get; set; }
         DbSet<REF_StatutDemandes> REF_StatutDemandes { get; set; }
         DbSet<REF_TypeCheckList> REF_TypeCheckList { get; set; }
         DbSet<REF_TypeEngin> REF_TypeEngin { get; set; }
         DbSet<ReponseDemande> ReponseDemande { get; set; }
         DbSet<ResultatControleDetail> ResultatControleDetail { get; set; }
         DbSet<ResultatControleEntete> ResultatControleEntete { get; set; }
         DbSet<AppFile> AppFile { get; set; }
         DbSet<AspNetRoles> AspNetRoles { get; set; }
         DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
         DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
         DbSet<AspNetUsers> AspNetUsers { get; set; }
         DbSet<Entite> Entite { get; set; }
         DbSet<Profile> Profile { get; set; }
         DbSet<Sites> Sites { get; set; }
    }
}
