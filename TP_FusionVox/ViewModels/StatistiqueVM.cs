using TP_FusionVox.ViewModels;
using TP_FusionVox.Models;

namespace TP_FusionVox.ViewModels
{
    public class StatistiqueVM
    {
        public int NbTotalArtistes { get; set; }
        public int NbTotalAbonnees { get; set; }
        public int NbTotalChansons { get; set; }
        
        public List<StatistiqueGenresMusicauxVM>? StatsGenresMusicaux { get; set; }
        public List<StatistiqueAgentVM>? StatistiqueAgents { get; set; }

        public int NbTotalAgent { get; set; }
        public float SalaireMoyenAgents { get; set; }
        public Concert? ConcertSucces { get; set; }
        public decimal prixMoyenConcert { get; set; }
    }
}
