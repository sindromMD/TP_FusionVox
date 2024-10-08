﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using TP_FusionVox.Models;

namespace TP_FusionVox.Models.Data
{
    public class TP_FusionVoxDbContext :IdentityDbContext<IdentityUser>
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("scaffolding");
            }
        }

        public DbSet<Artiste> Artistes { get; set; }
        public DbSet<GenreMusical> genresMusicaux { get; set; }
        public DbSet<Concert>? Concert { get; set; }
        public DbSet<Agent> Agent { get; set; }
        public DbSet<DonneesConfidentiellesAgent> DonneesConfidentiellesAgent { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public TP_FusionVoxDbContext(DbContextOptions<TP_FusionVoxDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //éviter les erreurs lors de la génération de pages Razor
            base.OnModelCreating(modelBuilder);

            //Générer des données de départ
            modelBuilder.GenerateData();

            modelBuilder.Entity<Concert>()
                   .Property(c => c.PrixBillet)
                   .HasColumnType("decimal(18, 2)"); // spécifie que la colonne dans la base de données sera de type décimal avec une précision totale de 18 chiffres et 2 décimales.
        }

 
    }
}
