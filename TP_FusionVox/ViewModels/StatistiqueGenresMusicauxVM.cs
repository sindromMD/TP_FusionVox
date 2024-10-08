﻿using TP_FusionVox.Models;

namespace TP_FusionVox.ViewModels
{
    public class StatistiqueGenresMusicauxVM
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public bool EstDisponible { get; set; }
        public int NbChansonsPubliees { get; set; }
        public int NbArtistes { get; set; }
        public int NbAbonnees { get; set; }
        public IReadOnlyList<Artiste> ListArtisteGenreMusical { get; set; } = new List<Artiste>();
    }
}
