using System.Security.Policy;
using TP_FusionVox.Models;
using TP_FusionVox.ViewModels;

namespace TP_FusionVox.Services
{
    public interface IGenreMusicalService : IServiceBaseAsync<GenreMusical>
    {
        Task<StatistiqueGenresMusicauxVM> StatistiquesUnGenreMusicalAsync(GenreMusical genre);
        Task<StatistiqueVM> StatistiquesTousGenresMusicauxAsync();

        public bool ADesArtistesAssocies(int id);
    }
}
