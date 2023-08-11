using TP_FusionVox.Models;

namespace TP_FusionVox.ViewModels
{
    public class FavorisViewModel
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public string Nom { get; set; }
        public string GenreMusical { get; set; }
        public int NbAbonnees { get; set; }
    }
}
