using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }

        public DbSet<ProgrammingTechnology> ProgrammingTechnologies { get; set; }

        public DbSet<User> Users { get; set; }
        
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        public DbSet<OperationClaim> OperationClaims { get; set; }


        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //    base.OnConfiguring(
            //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgrammingLanguage>(a =>
            {
                a.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasMany(a => a.ProgrammingTechnologies);
            });

            modelBuilder.Entity<ProgrammingTechnology>(a =>
            {
                a.ToTable("ProgrammingTechnologies").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Id).HasColumnName("ProgrammingLanguageId");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasOne(a => a.ProgrammingLanguage);
            });

            modelBuilder.Entity<User>(a =>
            {
                a.ToTable("Users").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.LastName).HasColumnName("LastName");
                a.Property(p => p.FirstName).HasColumnName("FirstName");
                a.Property(p => p.Email).HasColumnName("Email");
                a.Property(p => p.Status).HasColumnName("Status");
                a.Property(p => p.AuthenticatorType).HasColumnName("AuthenticatorType");
                a.Property(p => p.PasswordHash).HasColumnName("PasswordHash");
                a.Property(p => p.PasswordSalt).HasColumnName("PasswordSalt");
                a.HasMany(p => p.RefreshTokens);
                a.HasMany(p => p.UserOperationClaims);
            });
            modelBuilder.Entity<OperationClaim>(a =>
            {
                a.ToTable("OperationClaims").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
            });
            modelBuilder.Entity<UserOperationClaim>(a =>
            {
                a.ToTable("UserOperationClaims").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.UserId).HasColumnName("UserId");
                a.Property(p => p.OperationClaimId).HasColumnName("OperationClaimdId");
                a.HasOne(p => p.User);
                a.HasOne(p => p.OperationClaim);

            });

            modelBuilder.Entity<UserGitHub>(a =>
            {
                a.ToTable("UserGitHubs").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.UserId).HasColumnName("UserId");
                a.Property(p => p.ProfileUrl).HasColumnName("ProfileUrl");
                a.HasOne(p => p.User);
            });


            ProgrammingLanguage[] programmingLanguageSeeds = { new(1, "C#"), new(2, "Java") , new(3,"Python") };
            modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguageSeeds);


            ProgrammingTechnology[] programmingTechnologySeeds = { new(1,1,"WPF"),new(2,2,"Spring"),new(3,1,"ASP.NET") };
            modelBuilder.Entity<ProgrammingTechnology>().HasData(programmingTechnologySeeds);


        }
    }
}
