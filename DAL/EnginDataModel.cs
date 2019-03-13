namespace DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using DAL.Configurations;

    public partial class EnginDbContext : DbContext
    {
        public EnginDbContext()
            : base("name=EnginDataModel")
        {
        }

        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<CheckListExigence> CheckListExigence { get; set; }
        public virtual DbSet<CheckListRubrique> CheckListRubrique { get; set; }
        public virtual DbSet<DemandeAccesEngin> DemandeAccesEngin { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<ResultatExigence> ResultatExigence { get; set; }
        public virtual DbSet<TypeCheckList> TypeCheckList { get; set; }
        public virtual DbSet<AppFile> AppFile { get; set; }
        public virtual DbSet<InfoGenerale> InfoGenerale { get; set; }
        public virtual DbSet<InfoGeneralRubrique> InfoGeneralRubrique { get; set; }
        public virtual DbSet<ResultatInfoGenerale> ResultatInfoGenerale { get; set; }
        public virtual DbSet<TypeEngin> TypeEngin { get; set; }
        public virtual DbSet<NatureMatiere> NatureMatiere { get; set; }
        public virtual DbSet<Site> Site { get; set; }
        public virtual DbSet<Entity> Entity { get; set; }
        public virtual DbSet<DemandeResultatEntete> DemandeResultatEntete { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InfoGenerale>()
                .HasMany(e => e.TypeCheckList)
                .WithMany(e => e.InfoGenerale)
                .Map(m => m.ToTable("MAP_InfoGenerale_TypeCheckList").MapLeftKey("InfoGeneraleId").MapRightKey("TypeCheckListId"));

            #region AspNetRoles

            modelBuilder.Entity<AspNetRoles>()
               .HasMany(e => e.AspNetUsers)
               .WithMany(e => e.AspNetRoles)
               .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.CheckListExigence)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
            .HasMany(e => e.InfoGenerale)
            .WithRequired(e => e.AspNetUsers)
            .HasForeignKey(e => e.CreatedBy)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.CheckListRubrique)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.TypeEngin)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.DemandeResultatEntete)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.InfoGeneralRubrique)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.DemandeAccesEngin)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.TypeCheckList)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasOptional(e => e.Profile)
                .WithRequired(e => e.AspNetUsers);

            // CreateBy Site
            modelBuilder.Entity<AspNetUsers>()
               .HasMany(e => e.Site)
               .WithRequired(e => e.AspNetUsers)
               .HasForeignKey(e => e.CreatedBy)
               .WillCascadeOnDelete(false);

            // CreateBy Entity
            modelBuilder.Entity<AspNetUsers>()
               .HasMany(e => e.Entity)
               .WithRequired(e => e.AspNetUsers)
               .HasForeignKey(e => e.CreatedBy)
               .WillCascadeOnDelete(false);


            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.NatureMatiere)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);
            #endregion


            modelBuilder.Entity<CheckListExigence>()
                .HasMany(e => e.ResultatExigence)
                .WithRequired(e => e.CheckListExigence)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InfoGenerale>()
                .HasMany(e => e.ResultatInfoGenerale)
                .WithRequired(e => e.InfoGenerale)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CheckListRubrique>()
                .HasMany(e => e.CheckListExigence)
                .WithRequired(e => e.CheckListRubrique)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InfoGeneralRubrique>()
                .HasMany(e => e.InfoGenerale)
                .WithRequired(e => e.InfoGeneralRubrique)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DemandeAccesEngin>()
                .HasMany(e => e.DemandeResultatEntete)
                .WithRequired(e => e.DemandeAccesEngin)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DemandeResultatEntete>()
                .HasMany(e => e.ResultatExigence)
                .WithRequired(e => e.DemandeResultatEntete)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DemandeAccesEngin>()
                .HasMany(e => e.ResultatInfoGenerale)
                .WithRequired(e => e.DemandeAccesEngin)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeCheckList>()
                .HasMany(e => e.CheckListRubrique)
                .WithRequired(e => e.TypeCheckList)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeCheckList>()
                .HasMany(e => e.DemandeAccesEngin)
                .WithRequired(e => e.TypeCheckList)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeEngin>()
                .HasMany(e => e.DemandeAccesEngin)
                .WithRequired(e => e.TypeEngin)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeCheckList>()
                .HasMany(e => e.TypeEngin)
                .WithRequired(e => e.TypeCheckList)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Site>()
                .HasMany(e => e.Entity)
                .WithRequired(e => e.Site)
                .WillCascadeOnDelete(false);

            #region NatureMatiere

            modelBuilder.Entity<NatureMatiere>()
               .HasMany(e => e.DemandeAccesEngin)
               .WithOptional(e => e.NatureMatiere)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeCheckList>()
               .HasMany(e => e.NatureMatiere)
               .WithRequired(e => e.TypeCheckList)
               .WillCascadeOnDelete(false);

            #endregion

        }
    }
}
