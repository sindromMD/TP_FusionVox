using Microsoft.AspNetCore.Mvc;
using TP2.Models;
using System.Linq;
using TP2.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using TP_FusionVox.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace TP2.Controllers
{

    public class ArtisteController : Controller
    {

        private TP_FusionVoxDbContext _baseDonnees { get; set; }

        public ArtisteController(TP_FusionVoxDbContext baseDonnees)
        {
            _baseDonnees = baseDonnees;
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
            var ListArtiste = await _baseDonnees.Artistes.Include(a => a.GenreMusical).ToListAsync();
            model.Resultat = ListArtiste.GroupBy(a => a.GenreMusical.Nom).ToList();

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
                return View("Detail", detailArtiste);
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
                return View(ArtisteVM);
            }
            else
            {
                //Edit
                ArtisteVM.Artiste = await _baseDonnees.Artistes.FindAsync(Id);
                if(ArtisteVM.Artiste == null)
                {
                    return View("NotFound");
                }
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
                    if (artisteVM.Artiste.Id == 0)
                    {
                        //create
                       await _baseDonnees.Artistes.AddAsync(artisteVM.Artiste);
                    }
                    else
                    {
                        //update
                       _baseDonnees.Artistes.Update(artisteVM.Artiste);
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
                if(artisteASupprimer == null)
                {
                    return View("NotFound");
                }

                _baseDonnees.Artistes.Remove(artisteASupprimer);
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
