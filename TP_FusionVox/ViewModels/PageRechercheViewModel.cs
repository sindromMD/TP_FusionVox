using TP2.Models;

namespace TP2.ViewModels
{
    public class PageRechercheViewModel
    {
        public CritereRechercheViewModel Criteres { get; set; }
        public IEnumerable<IGrouping<string, Artiste>> Resultat { get; set; }
    }
}
