using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TP_FusionVox.Models.Data;
using TP2.Models;
using TP2.ViewModels;

namespace TP2.Controllers
{
    public class GestionArtisteController : Controller
    {

        private TP_FusionVoxDbContext _baseDonnees { get; set; }

        public GestionArtisteController(TP_FusionVoxDbContext baseDonnees)
        {
            _baseDonnees = baseDonnees;
        }

        // %! CLEAN à la fin du projet TP1 / Ou réutilisation pour d'autres actions/fonctions



        //// GET: GestionArtisteController/Delete/5
        //[Route("GestionArtiste/Delete/{id:int}")]
        //[Route("Artiste/Delete/{id:int}")]
        //[Route("Delete/{id:int}")]
        //public ActionResult Delete(int id)
        //{
        //    Artiste artisteASupprimer = _baseDonnees.Artistes.Where(a=>a.Id == id).FirstOrDefault();
        //    if (artisteASupprimer != null)
        //    {
        //        return View(artisteASupprimer);
        //    }
        //    else
        //    { return View("NotFound"); }

        //}

        //// POST: GestionArtisteController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeletePost(int id, Artiste artistes)
        //{
        //    try
        //    {   //1.Supprimer l'artiste de la base de données

        //        Artiste artisteASupprimer = _baseDonnees.Artistes.Where(a=>a.Id == id).FirstOrDefault();
        //        artisteASupprimer.GenreMusical.Artistes.Remove(artisteASupprimer);
        //        _baseDonnees.Artistes.Remove(artisteASupprimer);

        //        //2.Supprimer un artiste de la liste des favoris
        //        //(un artiste supprimé de la BD doit également être supprimé de la liste des favoris)

        //        List<FavorisViewModel> artisteFavoris = HttpContext.Session.Get<List<FavorisViewModel>>("Artistes");
        //        if (artisteFavoris != null)
        //        {
        //            artisteFavoris.Remove(artisteFavoris.Where(f => f.Id == id).SingleOrDefault());
        //            if (artisteFavoris.Count == 0)
        //                artisteFavoris = null;
        //        }
        //        HttpContext.Session.Set<List<FavorisViewModel>>("Artistes", artisteFavoris);

        //        return RedirectToAction("Recherche","Artiste");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
