using Microsoft.EntityFrameworkCore;
using TP_FusionVox.Models;
using TP_FusionVox.Models.Data;
using TP_FusionVox.Utility;
using TP_FusionVox.ViewModels;

namespace TP_FusionVox.Services
{
    public class ArtisteService : ServiceBaseAsync<Artiste>, IArtisteService
    {
        public ArtisteService(TP_FusionVoxDbContext dbContext) : base(dbContext) { }

        //renvoie une liste de tous les artistes triés par nom, qui font partie d'un genre musical disponible
        public async Task<IReadOnlyList<Artiste>> ObtenirToutListAsync()
        {
            return await _dbContext.Set<Artiste>()
                .OrderBy(a => a.Nom)
                .Include(a => a.GenreMusical)
                .Where(g => g.GenreMusical.EstDisponible)
                .ToListAsync();
        }
        // Retourne une liste d'artistes correspondant à un genre de musique
        public async Task<IReadOnlyList<Artiste>> ObtenirToutParIdAsync(int genreMusicalID)
        {
            return await _dbContext.Artistes
                .Where(x => x.IdGenreMusical == genreMusicalID)
                .ToListAsync();
        }

        // Retourne l'artiste trouvé selon le nom dans le paramètre, où la taille des caractères est omise.
        public async Task<Artiste?> ObtenirArtisteParNomAsync(string nom)
        {
            return await _dbContext.Set<Artiste>()
                .Where(a => a.Nom.ToLower() == nom.ToLower())
                .FirstOrDefaultAsync();
        }

        // Renvoie une liste d'artistes, filtrée selon les critères reçus en paramètre
        public IEnumerable<Artiste> FiltrageArtiste(CritereRechercheViewModel criteres)
        {
            IEnumerable<Artiste> ListArtisteFiltre = _dbContext.Artistes.
                                Where(a => (criteres.EstGenreMusicalPOP && a.IdGenreMusical == AppConstants.PopID) 
                                || (criteres.EstGenreMusicalHipHop && a.IdGenreMusical == AppConstants.HipHopID) 
                                || (criteres.EstGenreMuscialElectro && a.IdGenreMusical == AppConstants.ElectroID));

            if (criteres.InputNomArtiste != null)
                ListArtisteFiltre = ListArtisteFiltre.Where(a => a.Nom.ToLower().Contains(criteres.InputNomArtiste.ToLower()));
            if (criteres.NbMinAbonnes.HasValue)
                ListArtisteFiltre = ListArtisteFiltre.Where(a => a.NbAbonnees >= criteres.NbMinAbonnes.Value);
            if (criteres.NbMaxAbonnes.HasValue)
                ListArtisteFiltre = ListArtisteFiltre.Where(a => a.NbAbonnees <= criteres.NbMaxAbonnes.Value);
            if (criteres.ChoiPourPersonnageVedette == "Oui")
                ListArtisteFiltre = ListArtisteFiltre.Where(a => a.EstVedette);
            if (criteres.ChoiPourPersonnageVedette == "Non")
                ListArtisteFiltre = ListArtisteFiltre.Where(a => !a.EstVedette);

            return ListArtisteFiltre;
        }

        public bool ArtisteNomExist(string nom)
        {
            return _dbContext.Artistes.Where(x => x.Nom == nom).Any(); ;
        }

        public async Task ClearConcertsAsync(int artistId)
        {
            var artist = await _dbContext.Artistes.Include(a => a.ListConcerts)
                                                 .FirstOrDefaultAsync(a => a.Id == artistId);
            if (artist != null)
            {
                artist.ListConcerts.Clear();
                await _dbContext.SaveChangesAsync();
            }
        }


    }
}
