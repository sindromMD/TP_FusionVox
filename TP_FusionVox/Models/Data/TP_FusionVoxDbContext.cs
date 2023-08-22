using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using TP_FusionVox.Models;

namespace TP_FusionVox.Models.Data
{
    public class TP_FusionVoxDbContext :DbContext
    {
        public DbSet<Artiste> Artistes { get; set; }
        public DbSet<GenreMusical> genresMusicaux { get; set; }
        public DbSet<Concert>? Concert { get; set; }
        public DbSet<Agent> Agent { get; set; }

        public TP_FusionVoxDbContext(DbContextOptions<TP_FusionVoxDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Générer des données de départ
            modelBuilder.GenerateData();

            modelBuilder.Entity<Concert>()
                   .Property(c => c.PrixBillet)
                   .HasColumnType("decimal(18, 2)"); // spécifie que la colonne dans la base de données sera de type décimal avec une précision totale de 18 chiffres et 2 décimales.
        }

 
    }
}
