using TP_FusionVox.Models;
using TP_FusionVox.Models.Data;

namespace TP_FusionVox.Services
{
    public class GenreMusicalService : ServiceBaseAsync<GenreMusical>, IGenreMusicalService
    {
        public GenreMusicalService(TP_FusionVoxDbContext dbContext) : base(dbContext) { }
    }
}
