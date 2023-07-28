using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TP2.Models;
using TP2.ViewModels;

namespace TP2.Controllers
{
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
        //Add
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
                TempData["ajoutRepete"] = $"L'artiste ( {nomArtiste} ) que vous souhaitez ajouter figure déjà dans la liste.";
            }
            else
                artisteFavoris.Add(_baseDonnees.Artistes.Where(a => a.Id == Id)
                    .Select(x => new FavorisViewModel() 
                    { Id = x.Id, ImageURL = x.ImageURL, Nom = x.Nom, GenreMusical = x.GenreMusical.Nom, NbAbonnees = x.NbAbonnees }).Single());

                HttpContext.Session.Set<List<FavorisViewModel>>("Artistes", artisteFavoris);


            return RedirectToAction("Favoris");
        }

        //Delete
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
                    artisteFavoris.Remove(artisteFavoris.Where(f => f.Id == Id).SingleOrDefault());
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
