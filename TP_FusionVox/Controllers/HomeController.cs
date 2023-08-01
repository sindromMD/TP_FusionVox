using Microsoft.AspNetCore.Mvc;
using TP2.Models;
using System.Linq;
using TP2.ViewModels;
using TP_FusionVox.ViewModels;
using TP_FusionVox.Models.Data;

namespace TP2.Controllers
{

    public class HomeController : Controller
    {
        private TP_FusionVoxDbContext _baseDonnees { get; set; }

        public HomeController(TP_FusionVoxDbContext baseDonnees)
        {
            _baseDonnees = baseDonnees;
        }
        [Route("")]
        [Route("Home")]
        public IActionResult Index()
        {
            StatistiqueVM statistiqueVM = new StatistiqueVM()
            {
                // remplissage de statistiques pour chaque genre musical
                StatsGenresMusicaux = _baseDonnees.genresMusicaux
                .GroupBy(g => new { g.Id, g.Nom, g.Description, g.ImageUrl, NbA = g.Artistes.Count, NbC = g.Artistes.Select(x => x.NbChansons).Sum(), NbAb = g.Artistes.Select(x => x.NbAbonnees).Sum() })
                .Select(std => new StatistiqueGenresMusicauxVM
                {
                    Id = std.Key.Id,
                    Nom = std.Key.Nom,
                    Description = std.Key.Description,
                    ImageUrl = std.Key.ImageUrl,
                    NbArtistes = std.Key.NbA,
                    NbChansonsPubliees = std.Key.NbC,
                    NbAbonnees = std.Key.NbAb,
                }).ToList(),
                //statistiques globales sur les totaux des entités de la société d'enregistrement
                NbTotalArtistes = _baseDonnees.Artistes.Count(),
                NbTotalAbonnees = _baseDonnees.Artistes.Select(a => a.NbAbonnees).Sum(),
                NbTotalChansons = _baseDonnees.Artistes.Select(a => a.NbChansons).Sum()

            };
            return View(statistiqueVM);
        }
        //GET Upsert
        [Route("GenreMusical/create")]
        [Route("GenreMusical/edit/{id:int}")]
        public IActionResult Upsert(int? id)
        {
            GenreMusical genreMusical = new GenreMusical();
            if (id == null || id == 0)
            {
                //create
                return View(genreMusical);
            }
            else
            {
                //Edit
                genreMusical = _baseDonnees.genresMusicaux.Find(id);
                if (genreMusical == null)
                {
                    return View("NotFound");
                }
                return View(genreMusical);
            }
        }

        //Post: Upsert
        [Route("GenreMusical/create")]
        [Route("GenreMusical/edit/{id:int}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert (GenreMusical genreMusical)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(genreMusical.Id == 0)
                    {
                        //create
                        
                        _baseDonnees.genresMusicaux.Add(genreMusical);
                    }
                    else
                    {
                        //Update
                        _baseDonnees.genresMusicaux.Update(genreMusical);
                    }
                    _baseDonnees.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(genreMusical);
            }
            catch
            {
                return View();
            }
        }
    }
}
