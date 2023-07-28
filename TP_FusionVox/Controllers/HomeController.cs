using Microsoft.AspNetCore.Mvc;
using TP2.Models;
using System.Linq;
using TP2.ViewModels;

namespace TP2.Controllers
{
    
    public class HomeController : Controller
    {
        private BaseDeDonnees _baseDonnees { get; set; }

        public HomeController(BaseDeDonnees baseDonnees)
        {
            _baseDonnees = baseDonnees;
        }
        [Route("")]
        [Route("Home")]
        public IActionResult Index()
        {
            List<StatistiqueVM> listGenresMusicaux = _baseDonnees.genresMusicaux
                .GroupBy(g => new { g.Id, g.Nom, g.Description, g.ImageUrl, NbA = g.Artistes.Count, NbC = g.Artistes.Select(x => x.NbChansons).Sum(), NbAb = g.Artistes.Select(x=>x.NbAbonnees).Sum() })
                .Select(std => new StatistiqueVM
                {
                    Id = std.Key.Id,
                    Nom = std.Key.Nom,
                    Description = std.Key.Description,
                    ImageUrl = std.Key.ImageUrl,
                    NbArtistes = std.Key.NbA,
                    NbChansonsPubliees = std.Key.NbC,
                    NbAbonnees = std.Key.NbAb,
                }).ToList();
            ViewData["infoCount"] = _baseDonnees.Artistes.Count();
            ViewData["infoNbAbonnees"] = _baseDonnees.Artistes.Select(a=>a.NbAbonnees).Sum();
            ViewData["infoNbChanson"] = _baseDonnees.Artistes.Select(a => a.NbChansons).Sum();

            return View(listGenresMusicaux);
        }
    }
}
