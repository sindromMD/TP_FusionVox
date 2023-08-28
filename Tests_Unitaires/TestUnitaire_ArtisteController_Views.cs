using Castle.Core.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using TP_FusionVox.Controllers;
using TP_FusionVox.Models;
using TP_FusionVox.Services;
using TP_FusionVox.ViewModels;

namespace Tests_Unitaires
{
    public class TestUnitaire_ArtisteController_Views
    {

        private Mock<IArtisteService> _artisteServiceMock;
        private Mock<IGenreMusicalService> _musicalGenreServiceMock;
        private Mock<IConcertService> _concertServiceMock;
        private Mock<IAgentService> _agentServiceMock;
        private Mock<IWebHostEnvironment> _webHostEnvironmentMock;
        private Mock<IStringLocalizer<ArtisteController>> _stringLocalizerMock;
        private Mock<ILogger<ArtisteController>> _loggerMock;
        private ArtisteController _artisteController;
        private List<Artiste> _artisteList;
        private GenreMusical genreMusicalTest;
        private Agent _agentTest;

        public TestUnitaire_ArtisteController_Views()
        {
            _artisteServiceMock = new Mock<IArtisteService>();
            _agentServiceMock = new Mock<IAgentService>();
            _musicalGenreServiceMock = new Mock<IGenreMusicalService>();
            _concertServiceMock = new Mock<IConcertService>();
            _loggerMock = new Mock<ILogger<ArtisteController>>();
            _webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            _stringLocalizerMock = new Mock<IStringLocalizer<ArtisteController>>();


            _artisteController = new ArtisteController(_artisteServiceMock.Object,
                                                       _musicalGenreServiceMock.Object,
                                                       _concertServiceMock.Object,
                                                       _agentServiceMock.Object,
                                                       _webHostEnvironmentMock.Object,
                                                       _stringLocalizerMock.Object,
                                                       _loggerMock.Object
                                                       );
            genreMusicalTest = new GenreMusical() { Id = 1, Nom = "POP" };
            _agentTest = new Agent() { Id = 1, Nom = "Alex Culaxiz", ImageURL = "", Pays = "Canada", DateNaissance = new DateTime(1999, 01, 01), SalaireMensuel = 3564.50F, Curriel = "Alexei@vox.ca", Biographie = "Le professionnalisme de Alexei Culaxiz " };

            _artisteList = new List<Artiste>()
            {
                 new Artiste() { Id = 1, Nom = "The Weeknd", IdGenreMusical = genreMusicalTest.Id, AgentReprId = 1,ImageURL = "The_Weeknd.png", Pays = "Canada", DebutCarrier = new DateTime(2010, 01, 01), NbAbonnees = 433, NbChansons = 7, EstVedette = false, Biographie = "Abel Tesfaye, dit The Weeknd" },
                 new Artiste() { Id = 2, Nom = "Miley Cyrus", IdGenreMusical = genreMusicalTest.Id, AgentReprId = 1, ImageURL = "Miley_Cyrus.png", Pays = "États-Unis", DebutCarrier = new DateTime(2001, 03, 08), NbAbonnees = 745, NbChansons = 12, EstVedette = false, Biographie = "Destiny Cyrus1, dite Miley Cyrus" },
                 new Artiste() { Id = 3, Nom = "Bruno Mars", IdGenreMusical = genreMusicalTest.Id, AgentReprId = 1, ImageURL = "Bruno_Mars.png", Pays = "États-Unis", DebutCarrier = new DateTime(2004, 06, 11), NbAbonnees = 519, NbChansons = 10, EstVedette = false, Biographie = "Peter Gene Hernandez, dit Bruno Mars" },
                 new Artiste() { Id = 4, Nom = "Adele", IdGenreMusical = genreMusicalTest.Id, AgentReprId = 1, ImageURL = "Adele.png", Pays = "Grande-Bretagne", DebutCarrier = new DateTime(2008, 09, 05), NbAbonnees = 399, NbChansons = 13, EstVedette = false, Biographie = "Adele Laurie Blue Adkins, dite Adele" },
            };

        }


        [Fact]
        public async void DetailParID_ObtenirParIdAsync_validId_ReturnView()
        {

            //ARANGE 
            //Constructeur
            _artisteServiceMock.Setup(a => a.ObtenirParIdAsync(It.IsAny<int>()))
                .ReturnsAsync(
               _artisteList[3]);//id=4 (Retourne un artiste de _artistList avec l'identifiant 4)

            //Act
            var result = await _artisteController.DetailParID(4); //obtenons le résultat

            //Assert
            _artisteServiceMock.Verify(a => a.ObtenirParIdAsync(4), Times.Once()); // On vérifie que la méthode a été appelée une fois

            ViewResult viewResult = result as ViewResult; //Vérification que le résultat est de type ViewResult

            Assert.Equal("Detail", viewResult.ViewName); // Le nom de la vue doit être === à Detail

            Assert.NotNull(viewResult.ViewData); // ViewData != null

            Assert.IsType<Artiste>(viewResult.Model); //  viewResult est de type === Artist

            var model = viewResult.ViewData.Model as Artiste;

            Assert.Equal(4, model.Id); // Enfin, on vérifie que l'identifiant du modèle est bien 4
        }

        [Fact]
        public async void DetailParNom_ObtenirArtisteParNomAsync_validId_ReturnView()
        {

            //ARANGE 
            //Constructeur
            _artisteServiceMock.Setup(a => a.ObtenirArtisteParNomAsync(It.IsAny<string>()))
                .ReturnsAsync(
               _artisteList[3]);//Nom = Adele (Retourne un artiste de _artistList avec nom = "Adele")

            //Act
            var result = await _artisteController.DetailParNom(_artisteList[3].Nom); //obtenons le résultat

            //Assert
            _artisteServiceMock.Verify(a => a.ObtenirArtisteParNomAsync(_artisteList[3].Nom), Times.Once()); // On vérifie que la méthode a été appelée une fois

            ViewResult viewResult = result as ViewResult; //Vérification que le résultat est de type ViewResult

            Assert.Equal("Detail", viewResult.ViewName); // Le nom de la vue doit être === à Detail

            Assert.NotNull(viewResult.ViewData); // ViewData != null

            Assert.IsType<Artiste>(viewResult.Model); //  viewResult est de type === Artist

            var model = viewResult.ViewData.Model as Artiste;

            Assert.Equal(_artisteList[3].Nom, model.Nom); // Enfin, on vérifie que l'identifiant du modèle est bien 4
        }

        [Fact]
        public async void DetailParID_ObtenirParIdAsync_InvalidID()
        {

            var result = await _artisteController.DetailParID(1);

            _artisteServiceMock.Verify(a => a.ObtenirParIdAsync(1), Times.Once());

            Assert.IsAssignableFrom<ViewResult>(result);

            ViewResult viewResult = result as ViewResult;

            Assert.Equal("NotFound", viewResult.ViewName);
        }
        [Fact]
        public async void DetailParNom_ObtenirArtisteParNomAsync_InvalidNom()
        {

            var result = await _artisteController.DetailParNom("A");

            _artisteServiceMock.Verify(a => a.ObtenirArtisteParNomAsync("A"), Times.Once());

            Assert.IsAssignableFrom<ViewResult>(result);

            ViewResult viewResult = result as ViewResult;

            Assert.Equal("NotFound", viewResult.ViewName);
        }

        [Fact]
        public async Task DeletePost_InvalidId_ReturnsNotFoundView()
        {
            // Arrange
            _artisteServiceMock.Setup(a => a.ObtenirParIdAsync(It.IsAny<int>())).ReturnsAsync((Artiste)null);

            // Act
            var result = await _artisteController.DeletePost(123);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("NotFound", viewResult.ViewName);
        }


        //Message : J'imagine que le test a été implémenté presque correctement.
        //Mais je ne comprends pas pourquoi il ne passe pas.
        //C'est au-delà de ma compréhension pour le moment.
        [Fact]
        public async void Delete_ModelStateValid_RedirectToView()
        {
            //Arrange 
            var artisteIdASupprimer = 4;
            var artisteASupprimer = _artisteList[3];

            _artisteServiceMock.Setup(a => a.ObtenirParIdAsync(artisteIdASupprimer)).ReturnsAsync(artisteASupprimer);

            //Act
            var result = await _artisteController.DeletePost(artisteIdASupprimer);

            //Assert
            _artisteServiceMock.Verify(a => a.ObtenirParIdAsync(artisteIdASupprimer), Times.Once());
            _artisteServiceMock.Verify(a => a.SupprimerAsync(artisteIdASupprimer), Times.Once());

            Assert.IsType<RedirectToActionResult>(result);
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.Equal("Recherche", redirectToActionResult.ActionName);
            Assert.Equal("Artiste", redirectToActionResult.ControllerName);
        }

        //Message : la logique du test semble correcte.
        //Mais je ne comprends pas pourquoi il ne passe pas.
        //C'est au-delà de ma compréhension pour le moment.
        [Fact]
        public async void Create_ModelState_Invalid_ReturnView()
        {
            var artisteVM = new NewArtisteVM
            {
                Artiste = new Artiste(),
                AncienneImage = "ancienneImage.jpg",
                NomInitial = "Initial Name",
                GenresSelectList = new List<SelectListItem>(),
                //SelectedConcertId = 1,
                SelectedConcertIds = new List<int> { 1, 2, 3 },
                ConsertsSelectList = new List<SelectListItem>(),
                AgentSelectList = new List<SelectListItem>()
            };

            _artisteServiceMock.Setup(a => a.ArtisteNomExist(It.IsAny<string>())).Returns(false);
            _artisteController.ModelState.AddModelError("Error", "Error");

            //Act
            var result = await _artisteController.Upsert(artisteVM);
            var viewResult = result as ViewResult;

            Assert.Equal("Upsert", viewResult.ViewName);
        }        
    }
}