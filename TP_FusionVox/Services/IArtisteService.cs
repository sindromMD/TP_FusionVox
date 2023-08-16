using TP_FusionVox.Models;
using TP_FusionVox.ViewModels;

namespace TP_FusionVox.Services
{
    public interface IArtisteService:IServiceBaseAsync<Artiste>
    {
        Task<IReadOnlyList<Artiste>> ObtenirToutParIdAsync(int genreMusicalID);
        Task<IReadOnlyList<Artiste>> ObtenirToutListAsync();
        IEnumerable<Artiste> FiltrageArtiste(CritereRechercheViewModel criteres);
        Task<Artiste?> ObtenirArtisteParNomAsync(string nom);
        bool ArtisteNomExist(string nom);
    }
}
