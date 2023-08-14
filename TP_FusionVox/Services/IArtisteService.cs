using TP_FusionVox.Models;

namespace TP_FusionVox.Services
{
    public interface IArtisteService:IServiceBaseAsync<Artiste>
    {
        public Task<IReadOnlyList<Artiste>> ObtenirToutParGenreMusicalAsync(int genreMusicalID);
        public Task<IReadOnlyList<Artiste>> GetAllIndexAsync();
        public bool ZombieNameExit(string name);
    }
}
