using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using TP2.Models;

namespace TP_FusionVox.Models.Data
{
    public class TP_FusionVoxDbContext :DbContext
    {
        public DbSet<Artiste> Artists { get; set; }
        public DbSet<GenreMusical> genreMusicals { get; set; }

        public TP_FusionVoxDbContext(DbContextOptions<TP_FusionVoxDbContext> options) : base(options)
        {
           
        }
    }
}
