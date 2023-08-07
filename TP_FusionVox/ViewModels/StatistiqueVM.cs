using TP_FusionVox.ViewModels;
using TP2.Models;

namespace TP2.ViewModels
{
    public class StatistiqueVM
    {
        public int NbTotalArtistes { get; set; }
        public int NbTotalAbonnees { get; set; }
        public int NbTotalChansons { get; set; }
        
        public List<StatistiqueGenresMusicauxVM>? StatsGenresMusicaux { get; set; }
    }
}
