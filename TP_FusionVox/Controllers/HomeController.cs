using Microsoft.AspNetCore.Mvc;
using TP2.Models;
using System.Linq;
using TP2.ViewModels;
using TP_FusionVox.ViewModels;
using TP_FusionVox.Models.Data;
using Microsoft.EntityFrameworkCore;
using TP_FusionVox.Utility;
using Microsoft.AspNetCore.Hosting;

namespace TP2.Controllers
{

    public class HomeController : Controller
    {
        private TP_FusionVoxDbContext _baseDonnees { get; set; }
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(TP_FusionVoxDbContext baseDonnees, IWebHostEnvironment webHostEnvironment)
        {
            _baseDonnees = baseDonnees;
            _webHostEnvironment = webHostEnvironment;
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
            GenreMusicalVM? genreMusicalVM = new GenreMusicalVM();
            if (id == null || id == 0)
            {
                //create
                genreMusicalVM.GenreMusical = new GenreMusical();
                return View(genreMusicalVM);
            }
            else
            {
                //Edit
                genreMusicalVM.GenreMusical = await _baseDonnees.genresMusicaux.FindAsync(id);
                genreMusicalVM.AncienneImage = genreMusicalVM.GenreMusical.ImageUrl;  //permet de mettre en valeur l'image de l'artiste dans AncienneImage
                if (genreMusicalVM.GenreMusical == null)
                {
                    return View("NotFound");
                }
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
                        await _baseDonnees.genresMusicaux.AddAsync(genreMusicalVM.GenreMusical);
                        TempData[AppConstants.Success] = $"Le genre de la musique {genreMusicalVM.GenreMusical.Nom} a été ajouté.";
                    }
                    else
                    {
                        //Update
                        genreMusicalVM.GenreMusical.ImageUrl = TelechargerImageEtObtenirURL(genreMusicalVM.AncienneImage);
                        _baseDonnees.genresMusicaux.Update(genreMusicalVM.GenreMusical);
                        TempData[AppConstants.Success] = $"Les renseignements sur le genre musical {genreMusicalVM.GenreMusical.Nom} ont été modifiés.";
                    }
                    await _baseDonnees.SaveChangesAsync();
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
            GenreMusicalVM genreMusicalVM =new GenreMusicalVM();
            genreMusicalVM.GenreMusical = await _baseDonnees.genresMusicaux.FindAsync(id);

            if(genreMusicalVM.GenreMusical != null) 
            {
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
        public async Task<IActionResult> DeletePost (int id)
        {
            GenreMusical? genreMusical = await _baseDonnees.genresMusicaux.FindAsync(id);
            if(genreMusical == null)
            { 
                return View("NotFound"); 
            }
            _baseDonnees.genresMusicaux.Remove(genreMusical);

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
            await _baseDonnees.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
