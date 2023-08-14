using Microsoft.EntityFrameworkCore;
using TP_FusionVox.Models;
using TP_FusionVox.Models.Data;
using TP_FusionVox.ViewModels;

namespace TP_FusionVox.Services
{
    public class GenreMusicalService : ServiceBaseAsync<GenreMusical>, IGenreMusicalService
    {
        public GenreMusicalService(TP_FusionVoxDbContext dbContext) : base(dbContext) { }
       
        public async Task<StatistiqueVM> StatistiquesGenresMusicauxAsync()
        {
            StatistiqueVM statistiqueVM = new StatistiqueVM()
            {
                // remplissage de statistiques pour chaque genre musical
                StatsGenresMusicaux = await _dbContext.genresMusicaux
               .GroupBy(g => new
               {
                   g.Id,
                   g.Nom,
                   g.Description,
                   g.ImageUrl,
                   g.EstDisponible,
                   NbA = g.Artistes.Count,
                   NbC = g.Artistes.Select(x => x.NbChansons).Sum(),
                   NbAb = g.Artistes.Select(x => x.NbAbonnees).Sum()
               })
               .Select(std => new StatistiqueGenresMusicauxVM
               {
                   Id = std.Key.Id,
                   Nom = std.Key.Nom,
                   Description = std.Key.Description,
                   EstDisponible = std.Key.EstDisponible,
                   ImageUrl = std.Key.ImageUrl,
                   NbArtistes = std.Key.NbA,
                   NbChansonsPubliees = std.Key.NbC,
                   NbAbonnees = std.Key.NbAb,
               }).ToListAsync(),
                //statistiques globales sur les totaux des entités de la société d'enregistrement
                NbTotalArtistes = await _dbContext.Artistes.CountAsync(),
                NbTotalAbonnees = await _dbContext.Artistes.Select(a => a.NbAbonnees).SumAsync(),
                NbTotalChansons = await _dbContext.Artistes.Select(a => a.NbChansons).SumAsync()

            };
            return statistiqueVM;
        }

        public bool ADesArtistesAssocies(int id)
        {
            var associatedArtiste = _dbContext.Artistes.Where(x => x.IdGenreMusical == id).Any();
            return associatedArtiste;
        }

        public override async Task SupprimerAsync(int id)
        {
            var genreMusical = await this.ObtenirParIdAsync(id);
            if (genreMusical == null)
                return;
            if (ADesArtistesAssocies(id))
            {
                genreMusical.EstDisponible = false;
                await this.EditerAsync(genreMusical);
            }
            else
            {
                await base.SupprimerAsync(id);
            }
        }

    }
}
