using TP_FusionVox.Models;

namespace TP_FusionVox.ViewModels
{
    public class PageRechercheViewModel
    {
        public CritereRechercheViewModel Criteres { get; set; }
        public IEnumerable<IGrouping<string, Artiste>> Resultat { get; set; }
    }
}
