using Microsoft.AspNetCore.Mvc;
using TP2.Models;
using System.Linq;
using TP2.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TP2.Controllers
{

    public class ArtisteController : Controller
    {

        private BaseDeDonnees _baseDonnees { get; set; }

        public ArtisteController(BaseDeDonnees baseDonnees)
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
            model.Resultat = _baseDonnees.Artistes.GroupBy(a => a.GenreMusical.Nom);


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

            modelRecherche.Resultat = ListArtisteFiltre.GroupBy(a => a.GenreMusical.Nom);
            if(modelRecherche.Resultat.Count() == 0)
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
        // GET:Create
        [Route("artiste/create")]
        public IActionResult Create()
        {
            NewArtisteVM newArtisteVM = new NewArtisteVM();
            newArtisteVM.GenresSelectList = _baseDonnees.genresMusicaux.Select(gm => new SelectListItem
            {
                Text = gm.Nom,
                Value = gm.Id.ToString()
            }).OrderBy(gm => gm.Text); 
            return View(newArtisteVM);
        }

        // POST: Create
        [Route("artiste/create")]
        [HttpPost]
        public IActionResult Create(NewArtisteVM newArtiste)
        {
            if (ModelState.IsValid)
            {
                Artiste artiste = new Artiste()
                {
                    GenreMusical = _baseDonnees.genresMusicaux.Where(g => g.Id == newArtiste.GenreMusicalId).First(),
                    ImageURL = newArtiste.ImageURL,
                    Nom = newArtiste.Nom,
                    Pays = newArtiste.Pays,
                    Agent = newArtiste.Agent,
                    DebutCarrier = (DateTime)newArtiste.DebutCarrier,
                    NbChansons = (int)newArtiste.NbChansons,
                    Biographie = newArtiste.Biographie,
                    EstVedette = newArtiste.EstVedette,
                    Id = _baseDonnees.Artistes.LastOrDefault().Id + 1,
                };
                
            _baseDonnees.Artistes.Add(artiste);
            artiste.GenreMusical.Artistes.Add(artiste);
                
                return RedirectToAction("Recherche");
            }
            newArtiste.GenresSelectList = _baseDonnees.genresMusicaux.Select(gm => new SelectListItem
            {
                Text = gm.Nom,
                Value = gm.Id.ToString()
            }).OrderBy(gm => gm.Text);
            return View(newArtiste);

        }

        

    }
}
