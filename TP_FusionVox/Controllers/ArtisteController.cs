using Microsoft.AspNetCore.Mvc;
using TP_FusionVox.Models;
using System.Linq;
using TP_FusionVox.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using TP_FusionVox.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TP_FusionVox.Utility;
using Microsoft.Extensions.Localization;

namespace TP_FusionVox.Controllers
{

    public class ArtisteController : Controller
    {

        private TP_FusionVoxDbContext _baseDonnees { get; set; }
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<ArtisteController> _localizer;

        public ArtisteController(TP_FusionVoxDbContext baseDonnees,
                                    IWebHostEnvironment webHostEnvironment,
                                    IStringLocalizer<ArtisteController> localizer)
        {
            _baseDonnees = baseDonnees;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        [Route("artiste")]
        [Route("artiste/recherche")]
        public async Task<IActionResult> Recherche()
        {
            //IEnumerable<IGrouping<string, Artiste>> ListeArtisteByGenreMusical = _baseDonnees.Artistes.GroupBy(a => a.GenreMusical.Nom);
            var model = new PageRechercheViewModel();
            model.Criteres = new CritereRechercheViewModel();
            model.Criteres.EstGenreMusicalPOP = true;
            model.Criteres.EstGenreMusicalHipHop = true;
            model.Criteres.EstGenreMuscialElectro = true;
            //model.Resultat = _baseDonnees.Artistes.AsEnumerable().GroupBy(a => a.GenreMusical.Nom);
            var ListArtiste = await _baseDonnees.Artistes.Include(a => a.GenreMusical).Where(g=>g.GenreMusical.EstDisponible).ToListAsync();
            model.Resultat = ListArtiste.GroupBy(a => a.GenreMusical.Nom).ToList();
            ViewData["Title"] = this._localizer["ArtisteListTitle"];
            return View(model);
        }

        [Route("artiste/filtre")]
        public IActionResult Filtrage(CritereRechercheViewModel criteres)
        {
            var modelRecherche = new PageRechercheViewModel();
            modelRecherche.Criteres = criteres;

            IEnumerable<Artiste> ListArtisteFiltre = _baseDonnees.Artistes.Where(a => (criteres.EstGenreMusicalPOP && a.IdGenreMusical == 1) || (criteres.EstGenreMusicalHipHop && a.IdGenreMusical == 2) || (criteres.EstGenreMuscialElectro && a.IdGenreMusical == 3));

            if (criteres.InputNomArtiste != null)
                ListArtisteFiltre = ListArtisteFiltre.Where(a => a.Nom.ToLower().Contains(criteres.InputNomArtiste.ToLower()));
            if (criteres.NbMinAbonnes.HasValue)
                ListArtisteFiltre = ListArtisteFiltre.Where(a => a.NbAbonnees >= criteres.NbMinAbonnes.Value);
            if (criteres.NbMaxAbonnes.HasValue)
                ListArtisteFiltre = ListArtisteFiltre.Where(a => a.NbAbonnees <= criteres.NbMaxAbonnes.Value);
            if (criteres.ChoiPourPersonnageVedette == "Oui")
                ListArtisteFiltre = ListArtisteFiltre.Where(a => a.EstVedette);
            if (criteres.ChoiPourPersonnageVedette == "Non")
                ListArtisteFiltre = ListArtisteFiltre.Where(a => !a.EstVedette);

            modelRecherche.Resultat = ListArtisteFiltre.AsEnumerable().GroupBy(a => a.GenreMusical.Nom);
            if (modelRecherche.Resultat.Count() == 0)
                TempData["NotFound"] = $"Malheureusement, l'artiste {criteres.InputNomArtiste} que vous recherchez ne figure pas sur notre liste. ";

            return View("Recherche", modelRecherche);
        }


        [Route("artiste/detail/{id:int}")]
        [Route("artiste/{id:int}")]
        [Route("{id:int}")]
        public async Task<IActionResult> DetailParID(int id)
        {

            //Artiste detailArtiste = null;
            Artiste? detailArtiste =await _baseDonnees.Artistes.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (detailArtiste != null)
            {
                ViewData["Title"] = this._localizer["ArtisteDetailTitle"];
                return View("Detail", detailArtiste);
            }
            else
            { return View("NotFound"); }

        }


        [Route("artiste/detail/{Nom}")]
        [Route("artiste/{Nom}")]
        [Route("{Nom}")]
        public async Task<IActionResult> DetailParNom(string Nom)
        { 
            Artiste? detailArtiste = await _baseDonnees.Artistes.Where(a => a.Nom.ToLower() == Nom.ToLower()).FirstOrDefaultAsync();
            if (detailArtiste != null)
            {
                ViewData["Title"] = this._localizer["ArtisteDetailTitle"];
                return View("Detail", detailArtiste);
            }
                
            else
                return View("NotFound");

        }
        //Get UPSERT
        [Route("artiste/create")]
        [Route("artiste/upsert/create")]
        [Route("artiste/edit/{id:int}")]
        [Route("artiste/upsert/edit/{id:int}")]
        public async Task<IActionResult> Upsert(int? Id)
        {
            NewArtisteVM ArtisteVM = new NewArtisteVM();
            ArtisteVM.GenresSelectList = _baseDonnees.genresMusicaux.Select(gm => new SelectListItem
            
            {
                Text = gm.Nom,
                Value = gm.Id.ToString()
            }).OrderBy(gm => gm.Text);

            if (Id == null || Id == 0)
            {
                //create
                ArtisteVM.Artiste = new Artiste();
                ViewData["Title"] = this._localizer["ArtisteCreateTitle"];
                return View(ArtisteVM);
            }
            else
            {
                //Edit
                ArtisteVM.Artiste = await _baseDonnees.Artistes.FindAsync(Id);
                
                if (ArtisteVM.Artiste == null)
                {
                    return View("NotFound");
                }
                ArtisteVM.AncienneImage = ArtisteVM.Artiste.ImageURL;//permet de mettre en valeur l'image de l'artiste dans AncienneImage
                ViewData["Title"] = this._localizer["ArtisteEditTitle"];
                return View(ArtisteVM);

            }
        }

        // POST: UPSERT 
        [Route("artiste/create")]
        [Route("artiste/upsert/create")]
        [Route("artiste/edit/{id:int}")]
        [Route("artiste/upsert/edit/{id:int}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(NewArtisteVM artisteVM)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    string webRootPath = _webHostEnvironment.WebRootPath; //Chemin des images de Artiste
                    var files = HttpContext.Request.Form.Files; // Nouvelle image récupérée

                    // Télécharger l'image et obtenons le chemin d'accès à cette image.
                    string TelechargerImageEtObtenirURL(string? ancienneImageURL)
                    {
                        if (files.Count > 0)
                        {
                            //Générer un nom de fichier, qui est unique
                            string fileName = Guid.NewGuid().ToString();
                            //Trouver le chemin pour uploader les images de l'artiste, en combinant les path
                            var uploads = Path.Combine(webRootPath, AppConstants.ImagePathArtisteCtrl);
                            // extraire l'extention du fichier
                            var extension = Path.GetExtension(files[0].FileName);

                            if (ancienneImageURL != artisteVM.Artiste.ImageURL)
                            {
                                //L'image est modifiée: l'ancienne doit être supprimée
                                var PreviousImage = Path.Combine(webRootPath, AppConstants.ImagePathArtisteCtrl, ancienneImageURL.TrimStart('\\'));
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

                    if (artisteVM.Artiste.Id == 0)
                    {
                         //create
                         artisteVM.Artiste.ImageURL = TelechargerImageEtObtenirURL(null);
                         await _baseDonnees.Artistes.AddAsync(artisteVM.Artiste);
                         TempData[AppConstants.Success] = $"L'artiste {artisteVM.Artiste.Nom} a été ajouté.";
                    }
                    else
                    {
                        //update
                        artisteVM.Artiste.ImageURL = TelechargerImageEtObtenirURL(artisteVM.AncienneImage);
                        _baseDonnees.Artistes.Update(artisteVM.Artiste);
                        TempData[AppConstants.Success] = $"Les renseignements sur l'artiste {artisteVM.Artiste.Nom} ont été modifiés.";
                    }
                    await _baseDonnees.SaveChangesAsync();
                    return RedirectToAction("Recherche");
                }
                artisteVM.GenresSelectList = _baseDonnees.genresMusicaux.Select(gm => new SelectListItem
                {
                    Text = gm.Nom,
                    Value = gm.Id.ToString()
                }).OrderBy(gm => gm.Text);
                return View(artisteVM);
            }
            catch
            {
                return View();
            }

        }

        // GET: Artiste/Delete/5
        [Route("Artiste/Delete/{id:int}")]
        [Route("Artiste/Supprimer/{id:int}")]
        [Route("Delete/{id:int}")]
        [Route("Supprimer/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            NewArtisteVM artisteVM = new NewArtisteVM();

            artisteVM.Artiste =await _baseDonnees.Artistes.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (artisteVM.Artiste != null)
            {
                ViewData["Title"] = this._localizer["ArtisteDeleteTitle"];
                return View(artisteVM.Artiste);
            }
            else
            { 
                return View("NotFound");
            }

        }

        // POST: delete
        [Route("Artiste/DeletePost")]
        [Route("Artiste/Supprimer")]
        [Route("DeletePost")]
        [Route("Supprimer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int Id)
        {
            try
            {   //1.Supprimer l'artiste de la base de données

                Artiste? artisteASupprimer = await _baseDonnees.Artistes.Where(a => a.Id == Id).FirstOrDefaultAsync();
                if (artisteASupprimer == null)
                {
                    return View("NotFound");
                }
         
                _baseDonnees.Artistes.Remove(artisteASupprimer);

                // Supprimer les images du fichier
                string webRootPath = _webHostEnvironment.WebRootPath; //Chemin des images de zombies
                if (artisteASupprimer.ImageURL != null)
                {
                    var pathImage = Path.Combine(webRootPath, AppConstants.ImagePathArtisteCtrl, artisteASupprimer.ImageURL.TrimStart('\\'));
                    if (System.IO.File.Exists(pathImage))
                    {
                        System.IO.File.Delete(pathImage);
                    }
                }

                TempData[AppConstants.Success] = $"L'artiste {artisteASupprimer.Nom} a été supprimé de la base de données.";
                await _baseDonnees.SaveChangesAsync();


                //2.Supprimer un artiste de la liste des favoris
                //(un artiste supprimé de la BD doit également être supprimé de la liste des favoris)

                List<FavorisViewModel>? artisteFavoris = HttpContext.Session.Get<List<FavorisViewModel>>("Artistes");
                if (artisteFavoris != null)
                {
                    artisteFavoris.Remove(artisteFavoris.Where(f => f.Id == Id).SingleOrDefault());
                    if (artisteFavoris.Count == 0)
                        artisteFavoris = null;
                }
                HttpContext.Session.Set<List<FavorisViewModel>>("Artistes", artisteFavoris);

                return RedirectToAction("Recherche", "Artiste");
            }
            catch
            {
                return View();
            }
        }
    }
}
