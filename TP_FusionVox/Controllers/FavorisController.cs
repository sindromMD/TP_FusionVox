using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TP_FusionVox.Utility;
using TP_FusionVox.Models;
using TP_FusionVox.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace TP_FusionVox.Controllers
{
    [Authorize]
    public class FavorisController : Controller
    {
        private BaseDeDonnees _baseDonnees { get; set; }
        public FavorisController(BaseDeDonnees baseDonnees)
        {
            _baseDonnees = baseDonnees;
        }
       //Index
        [Route("Favoris")]
        public IActionResult Favoris()
        {

            List<FavorisViewModel> artistesFavoris = HttpContext.Session.Get<List<FavorisViewModel>>("Artistes");
            _ = artistesFavoris != null ? ViewData["CountFavoris"] = artistesFavoris.Count() : ViewData["CountFavoris"] = 0;

            return View(artistesFavoris);
        }
        //Add- POST
        [Route("Favoris")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Favoris(int Id)
        {

            List<FavorisViewModel> artisteFavoris = HttpContext.Session.Get<List<FavorisViewModel>>("Artistes");
            if (artisteFavoris == null)
                artisteFavoris = new List<FavorisViewModel>();
            if (artisteFavoris.Any(a => a.Id == Id))
            {
                // Nous n'ajoutons pas le même artiste à la liste.
                string nomArtiste = artisteFavoris.Find(af => af.Id == Id).Nom;
                TempData[AppConstants.Info] = $"L'artiste {nomArtiste} que vous souhaitez ajouter figure déjà dans la liste.";
            }
            else
            {
                var artiste = _baseDonnees.Artistes.Where(a => a.Id == Id)
                    .Select(x => new FavorisViewModel()
                    { Id = x.Id, ImageURL = x.ImageURL, Nom = x.Nom, GenreMusical = x.GenreMusical.Nom, NbAbonnees = x.NbAbonnees }).Single();
                artisteFavoris.Add(artiste);
                TempData[AppConstants.Success] = $"L'artiste {artiste.Nom} a été ajouté.";
            }
                HttpContext.Session.Set<List<FavorisViewModel>>("Artistes", artisteFavoris);
                

            return RedirectToAction("Favoris");
        }

        //Delete-POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SupprimerFavoris(int Id)
        {

            try
            {
                List<FavorisViewModel> artisteFavoris = HttpContext.Session.Get<List<FavorisViewModel>>("Artistes");
                if (artisteFavoris != null )
                //artisteFavoris = new List<FavorisViewModel>();
                {
                    var artiste = artisteFavoris.Where(f => f.Id == Id).SingleOrDefault();
                    artisteFavoris.Remove(artiste);
                    TempData[AppConstants.Success] = $"L'artiste {artiste.Nom} a été supprimé de la base de données.";
                    if (artisteFavoris.Count == 0)
                        artisteFavoris = null;
                }
                HttpContext.Session.Set<List<FavorisViewModel>>("Artistes", artisteFavoris);
                
                return RedirectToAction("Favoris", artisteFavoris);
            }
            catch
            {
                return View();
            }
           
        }
    }
}
