using Microsoft.EntityFrameworkCore;
using TP_FusionVox.Models;
using TP_FusionVox.Models.Data;

namespace TP_FusionVox.Services
{
    public class ArtisteService : ServiceBaseAsync<Artiste>, IArtisteService
    {
        public ArtisteService (TP_FusionVoxDbContext dbContext) : base (dbContext) { }

        public Task<IReadOnlyList<Artiste>> GetAllIndexAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Artiste>> ObtenirToutParGenreMusicalAsync(int genreMusicalID)
        {
            return await _dbContext.Artistes.Where(x => x.IdGenreMusical == genreMusicalID).ToListAsync();
        }

        public bool ZombieNameExit(string name)
        {
            throw new NotImplementedException();
        }
    }
}
