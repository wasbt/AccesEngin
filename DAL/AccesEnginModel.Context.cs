﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OcpPerformanceDataContext : DbContext
    {
        public OcpPerformanceDataContext()
            : base("name=OcpPerformanceDataContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DemandeAccesEngin> DemandeAccesEngin { get; set; }
        public virtual DbSet<DemandeAccesEnginInfoGeneraleValue> DemandeAccesEnginInfoGeneraleValue { get; set; }
        public virtual DbSet<REF_CheckListExigence> REF_CheckListExigence { get; set; }
        public virtual DbSet<REF_CheckListRubrique> REF_CheckListRubrique { get; set; }
        public virtual DbSet<REF_InfoGenerale> REF_InfoGenerale { get; set; }
        public virtual DbSet<REF_InfoGeneralRubrique> REF_InfoGeneralRubrique { get; set; }
        public virtual DbSet<REF_NatureMatiere> REF_NatureMatiere { get; set; }
        public virtual DbSet<REF_StatutDemandes> REF_StatutDemandes { get; set; }
        public virtual DbSet<REF_TypeCheckList> REF_TypeCheckList { get; set; }
        public virtual DbSet<REF_TypeEngin> REF_TypeEngin { get; set; }
        public virtual DbSet<ReponseDemande> ReponseDemande { get; set; }
        public virtual DbSet<ResultatControleDetail> ResultatControleDetail { get; set; }
        public virtual DbSet<ResultatControleEntete> ResultatControleEntete { get; set; }
        public virtual DbSet<AppFile> AppFile { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Entite> Entite { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<Sites> Sites { get; set; }
    }
}
