namespace DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
                .HasMany(e => e.CheckListRubrique)
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

            modelBuilder.Entity<CheckListExigence>()
                .HasMany(e => e.ResultatExigence)
                .WithRequired(e => e.CheckListExigence)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CheckListRubrique>()
                .HasMany(e => e.CheckListExigence)
                .WithRequired(e => e.CheckListRubrique)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DemandeAccesEngin>()
                .HasMany(e => e.ResultatExigence)
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
        }
    }
}
