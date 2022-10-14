using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace OglasAutomobila.Data
{
    public partial class OglasiContext : IdentityDbContext<AspNetUser, AspNetRole, int, AspNetUserClaim, AspNetUserRole, AspNetUserLogin, AspNetRoleClaim, AspNetUserToken>
    {
        public OglasiContext()
        {
        }

        public OglasiContext(DbContextOptions<OglasiContext> options)
            : base(options)
        {

        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<Mesto> Mestos { get; set; }
        public virtual DbSet<Oglasi> Oglasis { get; set; }
        public virtual DbSet<OglasiUser> OglasiUsers { get; set; }
        public virtual DbSet<Image> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Oglasi;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");


            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.ProviderKey).ValueGeneratedOnAdd();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
              
                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany()
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Mesto>(entity =>
            {
                entity.ToTable("Mesto");

                entity.Property(e => e.MestoId).ValueGeneratedOnAdd();

                entity.Property(e => e.Grad).HasMaxLength(255);

                entity.Property(e => e.Opština).HasMaxLength(255);

                entity.Property(e => e.Ulica).HasMaxLength(255);
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Image");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.ImagePath).HasMaxLength(400);
                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<Oglasi>(entity =>
            {
                entity.ToTable("Oglasi");

                entity.Property(e => e.OglasiId).ValueGeneratedOnAdd();

                entity.Property(e => e.Godište).HasColumnType("datetime");

                entity.Property(e => e.Marka).HasMaxLength(255);

                entity.Property(e => e.Model).HasMaxLength(255);

                entity.Property(e => e.Naziv).HasMaxLength(255);

                entity.Property(e => e.Opis).HasMaxLength(255);

                entity.Property(e => e.RegistrovanDo).HasColumnType("datetime");

                entity.HasOne(d => d.Mesto)
                    .WithMany(p => p.Oglasis)
                    .HasForeignKey(d => d.MestoId)
                    .HasConstraintName("FK__Oglasi__MestoId__534D60F1");
                entity.HasOne(d => d.Image)
                .WithMany(p=>p.Oglasis)
                .HasForeignKey(d=>d.ImageId)
                .HasConstraintName("ImageId");                
            });

            modelBuilder.Entity<OglasiUser>(entity =>
            {
                entity.HasKey(e => e.OglasiUser1)
                    .HasName("PK__OglasiUs__5E21ABC9A86D6161");

                entity.ToTable("OglasiUser");

                entity.Property(e => e.OglasiUser1)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("OglasiUser");

                entity.HasOne(d => d.Oglasi)
                    .WithMany(p => p.OglasiUsers)
                    .HasForeignKey(d => d.OglasiId)
                    .HasConstraintName("FK__OglasiUse__Oglas__5629CD9C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OglasiUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__OglasiUse__UserI__571DF1D5");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
