using Microsoft.AspNetCore.Mvc;
using TP2.Models;
using System.Linq;
using TP2.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using TP_FusionVox.Models.Data;

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
        public IActionResult Recherche()
        {
            //IEnumerable<IGrouping<string, Artiste>> ListeArtisteByGenreMusical = _baseDonnees.Artistes.GroupBy(a => a.GenreMusical.Nom);
            var model = new PageRechercheViewModel();
            model.Criteres = new CritereRechercheViewModel();
            model.Criteres.EstGenreMusicalPOP = true;
            model.Criteres.EstGenreMusicalHipHop = true;
            model.Criteres.EstGenreMuscialElectro = true;
            model.Resultat = _baseDonnees.Artistes.AsEnumerable().GroupBy(a => a.GenreMusical.Nom);


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
        public IActionResult DetailParID(int id)
        {

            Artiste detailArtiste = null;
            detailArtiste = _baseDonnees.Artistes.Where(a => a.Id == id).FirstOrDefault();
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
        public IActionResult DetailParNom(string Nom)
        {
            Artiste detailArtiste = null;
            detailArtiste = _baseDonnees.Artistes.Where(a => a.Nom.ToLower() == Nom.ToLower()).FirstOrDefault();
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
        public IActionResult Upsert(int? Id)
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
                ArtisteVM.Artiste = _baseDonnees.Artistes.Find(Id);
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
        public IActionResult Upsert(NewArtisteVM artisteVM)
        {
            if (ModelState.IsValid)
            {
                if(artisteVM.Artiste.Id == 0)
                {
                    //create
                    _baseDonnees.Artistes.Add(artisteVM.Artiste);
                }
                else
                {
                    //update
                    _baseDonnees.Artistes.Update(artisteVM.Artiste);
                }
                _baseDonnees.SaveChanges();
                return RedirectToAction("Recherche");
            }
            artisteVM.GenresSelectList = _baseDonnees.genresMusicaux.Select(gm => new SelectListItem
            {
                Text = gm.Nom,
                Value = gm.Id.ToString()
            }).OrderBy(gm => gm.Text);
            return View(artisteVM);

        }
    }
}
