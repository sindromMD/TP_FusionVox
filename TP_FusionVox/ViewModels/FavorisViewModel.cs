using TP2.Models;

namespace TP2.ViewModels
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
