using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP_FusionVox.Models;
using TP_FusionVox.Models.Data;
using TP_FusionVox.ViewModels;

namespace TP_FusionVox.Services
{
    public class GenreMusicalService : ServiceBaseAsync<GenreMusical>, IGenreMusicalService
    {
        public GenreMusicalService(TP_FusionVoxDbContext dbContext) : base(dbContext) { }

        public async Task<StatistiqueGenresMusicauxVM> StatistiquesUnGenreMusicalAsync(GenreMusical genre)
        {
            var statistiqueGenre = await _dbContext.genresMusicaux
                .Where(g => g.Id == genre.Id)
                .Select(g => new StatistiqueGenresMusicauxVM
                {
                    Id = g.Id,
                    Nom = g.Nom,
                    Description = g.Description,
                    EstDisponible = g.EstDisponible,
                    ImageUrl = g.ImageUrl,
                    NbArtistes = g.Artistes.Count,
                    NbChansonsPubliees = g.Artistes.Select(x => x.NbChansons).Sum(),
                    NbAbonnees = g.Artistes.Select(x => x.NbAbonnees).Sum(),
                    ListArtisteGenreMusical = g.Artistes.Where(a=>a.GenreMusical.Id == genre.Id).ToList(),
                }).FirstOrDefaultAsync();

            return statistiqueGenre;
        }

        public async Task<StatistiqueVM> StatistiquesTousGenresMusicauxAsync()
        {
            var genres = await _dbContext.genresMusicaux.ToListAsync();
            StatistiqueVM statistiqueVM = new StatistiqueVM();
            statistiqueVM.StatsGenresMusicaux = new List<StatistiqueGenresMusicauxVM>();

            foreach (var genre in genres)
            {
                statistiqueVM.StatsGenresMusicaux.Add(await StatistiquesUnGenreMusicalAsync(genre));
            }
            
            //statistiques globales
            statistiqueVM.NbTotalArtistes = await _dbContext.Artistes.CountAsync();
            statistiqueVM.NbTotalAbonnees = await _dbContext.Artistes.Select(a => a.NbAbonnees).SumAsync();
            statistiqueVM.NbTotalChansons = await _dbContext.Artistes.Select(a => a.NbChansons).SumAsync();
            statistiqueVM.NbTotalAgent = await _dbContext.Agent.CountAsync();
            statistiqueVM.SalaireMoyenAgents = await _dbContext.Agent.Select(x => x.SalaireMensuel).AverageAsync();
            statistiqueVM.prixMoyenConcert = await _dbContext.Concert.Select(c => c.PrixBillet).AverageAsync();
            statistiqueVM.ConcertSucces = await _dbContext.Concert
                                         .OrderByDescending(c => c.NbBilletsVendu)
                                         .FirstOrDefaultAsync();

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

        public IEnumerable<SelectListItem> ListGenresMusicauxDisponible()
        {
            var genresMusicauxDisponibleList = _dbContext.genresMusicaux.Where(gm => gm.EstDisponible == true)
            .Select(x => new SelectListItem
            {
                Text = x.Nom,
                Value = x.Id.ToString()
            }).OrderBy(t => t.Text);

            return genresMusicauxDisponibleList;
        }
      
      

    }
}
