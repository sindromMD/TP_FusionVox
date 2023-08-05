using Microsoft.AspNetCore.Mvc;
using TP2.Models;
using System.Linq;
using TP2.ViewModels;
using TP_FusionVox.ViewModels;
using TP_FusionVox.Models.Data;
using Microsoft.EntityFrameworkCore;
using TP_FusionVox.Utility;

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
        public async Task<IActionResult> Index()
        {
            StatistiqueVM statistiqueVM = new StatistiqueVM()
            {
                // remplissage de statistiques pour chaque genre musical
                StatsGenresMusicaux =await _baseDonnees.genresMusicaux
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
                }).ToListAsync(),
                //statistiques globales sur les totaux des entités de la société d'enregistrement
                NbTotalArtistes = await _baseDonnees.Artistes.CountAsync(),
                NbTotalAbonnees = await _baseDonnees.Artistes.Select(a => a.NbAbonnees).SumAsync(),
                NbTotalChansons = await _baseDonnees.Artistes.Select(a => a.NbChansons).SumAsync()

            };
            return View(statistiqueVM);
        }
        //GET Upsert
        [Route("GenreMusical/create")]
        [Route("GenreMusical/edit/{id:int}")]
        public async Task<IActionResult> Upsert(int? id)
        {
            GenreMusical? genreMusical = new GenreMusical();
            if (id == null || id == 0)
            {
                //create
                return View(genreMusical);
            }
            else
            {
                //Edit
                genreMusical = await _baseDonnees.genresMusicaux.FindAsync(id);
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
        public async Task<IActionResult> Upsert(GenreMusical genreMusical)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (genreMusical.Id == 0)
                    {
                        //create

                        await _baseDonnees.genresMusicaux.AddAsync(genreMusical);
                        TempData[AppConstants.Success] = $"Le genre de la musique {genreMusical.Nom} a été ajouté.";
                    }
                    else
                    {
                        //Update
                        _baseDonnees.genresMusicaux.Update(genreMusical);
                        TempData[AppConstants.Success] = $"Les renseignements sur le genre musical {genreMusical.Nom} ont été modifiés.";
                    }
                    await _baseDonnees.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(genreMusical);
            }
            catch
            {
                return View();
            }
        }

        [Route("GenreMusical/Delete/{id:int}")]
        [Route("GenreMusical/Supprimer/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            GenreMusical? genreMusical = await _baseDonnees.genresMusicaux.FindAsync(id);
            if(genreMusical != null) 
            {
                return View(genreMusical);
            }
            else
                return View("NotFound");
            
        }

        [Route("GenreMusical/Delete")]
        [Route("GenreMusical/Supprimer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost (int id)
        {
            GenreMusical? genreMusical = await _baseDonnees.genresMusicaux.FindAsync(id);
            if(genreMusical == null)
            { 
                return View("NotFound"); 
            }
            _baseDonnees.genresMusicaux.Remove(genreMusical);
            TempData[AppConstants.Success] = $"Le genre de la musique {genreMusical.Nom} a été supprimer.";
            await _baseDonnees.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
