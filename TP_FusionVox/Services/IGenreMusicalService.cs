using System.Security.Policy;
using TP_FusionVox.Models;
using TP_FusionVox.ViewModels;

namespace TP_FusionVox.Services
{
    public interface IGenreMusicalService : IServiceBaseAsync<GenreMusical>
    {
        Task<StatistiqueVM> StatistiquesGenresMusicauxAsync();
        public bool ADesArtistesAssocies(int id);
    }
}
