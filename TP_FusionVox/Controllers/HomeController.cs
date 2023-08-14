using Microsoft.AspNetCore.Mvc;
using TP_FusionVox.Models;
using System.Linq;
using TP_FusionVox.ViewModels;
using TP_FusionVox.Models.Data;
using Microsoft.EntityFrameworkCore;
using TP_FusionVox.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using System.Diagnostics;
using TP_FusionVox.Services;

namespace TP_FusionVox.Controllers
{

    public class HomeController : Controller
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<HomeController> _localizer;
        private IGenreMusicalService _serviceGM { get; set; }
        private IArtisteService _serviceA { get; set; }

        public HomeController(  IGenreMusicalService serviceGM,
                                IArtisteService serviceA,
                                IWebHostEnvironment webHostEnvironment,
                                IStringLocalizer<HomeController> localizer)
        {
            //_baseDonnees = baseDonnees;
            _serviceGM = serviceGM;
            _serviceA = serviceA;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }
        [Route("")]
        [Route("Home")]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = this._localizer["IndexTitle"];
            return View(await _serviceGM.StatistiquesTousGenresMusicauxAsync());
        }
        [Route("GenreMusical/Details/{id:int}")]
        public async Task<IActionResult> DetailParID(int id)
        {
            var genreMusical = await _serviceGM.ObtenirParIdAsync(id);
            if (genreMusical != null)
            {
                StatistiqueGenresMusicauxVM DetailsArtisteVM = await _serviceGM.StatistiquesUnGenreMusicalAsync(genreMusical);
                DetailsArtisteVM.ListArtisteGenreMusical = await _serviceA.ObtenirToutParGenreMusicalAsync(id); ;
                ViewData["Title"] = this._localizer["DetailsTitle"];
                return View("Details", DetailsArtisteVM);
            }
            else
                return View("NotFound");
           
        }
        //GET Upsert 
        [Route("GenreMusical/create")]
        [Route("GenreMusical/edit/{id:int}")]
        public async Task<IActionResult> Upsert(int? id)
        {
            GenreMusicalVM? genreMusicalVM = new GenreMusicalVM();
            if (id == null || id == 0)
            {
                //create
                genreMusicalVM.GenreMusical = new GenreMusical();
                ViewData["Title"] = this._localizer["CreateTitle"];
                return View(genreMusicalVM);
            }
            else
            {
                //Edit
                genreMusicalVM.GenreMusical = await _serviceGM.ObtenirParIdAsync((int)id);

                if (genreMusicalVM.GenreMusical == null)
                {
                    return View("NotFound");
                }
                genreMusicalVM.AncienneImage = genreMusicalVM.GenreMusical.ImageUrl;  //permet de mettre en valeur l'image de l'artiste dans AncienneImage
                ViewData["Title"] = this._localizer["EditTitle"];
                return View(genreMusicalVM);
            }
        }

        //Post: Upsert
        [Route("GenreMusical/create")]
        [Route("GenreMusical/edit/{id:int}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(GenreMusicalVM genreMusicalVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string webRootPath = _webHostEnvironment.WebRootPath; //Chemin des images de GenreMusical
                    var files = HttpContext.Request.Form.Files; // Nouvelle image récupérée

                    // Télécharger l'image et obtenons le chemin d'accès à cette image.
                    string TelechargerImageEtObtenirURL(string? ancienneImageURL)
                    {
                        if (files.Count > 0)
                        {
                            //Générer un nom de fichier, qui est unique
                            string fileName = Guid.NewGuid().ToString();
                            //Trouver le chemin pour uploader les images du genre musical , en combinant les path
                            var uploads = Path.Combine(webRootPath, AppConstants.ImagePathGenreMusicalCtrl);
                            // extraire l'extention du fichier
                            var extension = Path.GetExtension(files[0].FileName);

                            if (ancienneImageURL != genreMusicalVM.GenreMusical.ImageUrl)
                            {
                                //L'image est modifiée: l'ancienne doit être supprimée
                                var PreviousImage = Path.Combine(webRootPath, AppConstants.ImagePathGenreMusicalCtrl, ancienneImageURL.TrimStart('\\'));
                                if (System.IO.File.Exists(PreviousImage))
                                {
                                    System.IO.File.Delete(PreviousImage);
                                }
                            }

                            //Create un cannal pour transférer le fichier
                            using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                            {
                                files[0].CopyTo(filesStreams);
                            }
                            //Composer un nom pour le fichier en ajoutant l'extension qui sera enregistrée dans la base de données
                            //avec le chemin relatif en tenant compte de la racine(Root).
                            // sans le path relatif (le path devra être ajouté dans la View)

                            return fileName + extension;
                        }
                        else
                        {
                            return ancienneImageURL;
                        }
                    }

                    if (genreMusicalVM.GenreMusical.Id == 0)
                    {
                        //create
                        genreMusicalVM.GenreMusical.ImageUrl = TelechargerImageEtObtenirURL(null);
                        await _serviceGM.CreerAsync(genreMusicalVM.GenreMusical);
                        TempData[AppConstants.Success] = $"Le genre de la musique {genreMusicalVM.GenreMusical.Nom} a été ajouté.";
                    }
                    else
                    {
                        //Update
                        genreMusicalVM.GenreMusical.ImageUrl = TelechargerImageEtObtenirURL(genreMusicalVM.AncienneImage);
                        await _serviceGM.EditerAsync(genreMusicalVM.GenreMusical);
                        TempData[AppConstants.Success] = $"Les renseignements sur le genre musical {genreMusicalVM.GenreMusical.Nom} ont été modifiés.";
                    }
                    return RedirectToAction("Index");
                }
                return View(genreMusicalVM);
            }
            catch
            {
                return View();
            }
        }

        //Delete - GET
        [Route("GenreMusical/Delete/{id:int}")]
        [Route("GenreMusical/Supprimer/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            GenreMusicalVM genreMusicalVM = new GenreMusicalVM();
            genreMusicalVM.GenreMusical = await _serviceGM.ObtenirParIdAsync(id);

            if (genreMusicalVM.GenreMusical != null)
            {
                ViewData["Title"] = this._localizer["DeleteTitle"];
                return View(genreMusicalVM.GenreMusical);
            }
            else
                return View("NotFound");

        }

        //Delete - POST
        [Route("GenreMusical/Delete")]
        [Route("GenreMusical/Supprimer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            GenreMusical? genreMusical = await _serviceGM.ObtenirParIdAsync(id);
            if (genreMusical == null)
            {
                return View("NotFound");
            }

            await _serviceGM.SupprimerAsync(id);

            // Supprimer les images du fichier
            string webRootPath = _webHostEnvironment.WebRootPath; //Chemin des images de zombies
            if (genreMusical.ImageUrl != null)
            {
                var pathImage = Path.Combine(webRootPath, AppConstants.ImagePathGenreMusicalCtrl, genreMusical.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(pathImage))
                {
                    System.IO.File.Delete(pathImage);
                }
            }

            TempData[AppConstants.Success] = $"Le genre de la musique {genreMusical.Nom} a été supprimer.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            var cookie = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture));
            var name = CookieRequestCultureProvider.DefaultCookieName;

            Response.Cookies.Append(name, cookie, new CookieOptions
            {
                Path = "/",
                Expires = DateTimeOffset.UtcNow.AddYears(1),
            });

            return LocalRedirect(returnUrl);
        }

    }
}
