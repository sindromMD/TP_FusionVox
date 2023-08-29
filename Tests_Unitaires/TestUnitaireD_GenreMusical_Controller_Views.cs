using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_FusionVox.Controllers;
using TP_FusionVox.Models;
using TP_FusionVox.Services;
using TP_FusionVox.ViewModels;

namespace Tests_Unitaires
{
    public class TestUnitaireD_GenreMusical_Controller_Views
    {
        private Mock<IGenreMusicalService> _genreMusicalService_Mock;
        private Mock<IArtisteService> _artisteService_Mock;
        private Mock<IWebHostEnvironment> _webHostEnvironment_Mock;
        private Mock<IStringLocalizer<GenreMusicalController>> _stringLocalizerMock;
        private GenreMusicalController _genreMusicalController;

        public TestUnitaireD_GenreMusical_Controller_Views()
        {
            _genreMusicalService_Mock = new Mock<IGenreMusicalService>();
            _artisteService_Mock = new Mock<IArtisteService>();
            _webHostEnvironment_Mock = new Mock<IWebHostEnvironment>();
            _stringLocalizerMock = new Mock<IStringLocalizer<GenreMusicalController>>();
            _genreMusicalController = new GenreMusicalController(
                _genreMusicalService_Mock.Object,
                _artisteService_Mock.Object,
                _webHostEnvironment_Mock.Object,
                _stringLocalizerMock.Object);
        }


        [Fact]
        public async void Create_ModelStateValid_RedirectToView()
        {
            //Arange
            var genreMusicalMock = new GenreMusical()
            {
                Id = 1,
                Nom = "POP",
                ImageUrl = "POP.jpg",
                Description = "La musique pop"
            };
            var genreMusicalVM = new GenreMusicalVM() { };
            genreMusicalVM.GenreMusical = new GenreMusical();
            _genreMusicalService_Mock.Setup(g => g.CreerAsync(It.IsAny<GenreMusical>())).ReturnsAsync(genreMusicalMock);
            var result = _genreMusicalController.Upsert(genreMusicalVM).Result;

            _genreMusicalService_Mock.Verify(g => g.CreerAsync(It.IsAny<GenreMusical>()), Times.Once());
            
            var viewResult = result as RedirectToActionResult;
            Assert.IsType<RedirectToActionResult>(viewResult);
            Assert.Equal("Index", viewResult.ActionName);
            Assert.Equal("Home", viewResult.ControllerName);         
            Assert.Null(viewResult.ControllerName);
        }
        [Fact]
        public async void DetailParId_ObtenirParIdAsync_RetunrView()
        {
            //arrange
            var genreMusical = new GenreMusical() { Id = 1, Nom = "POP" };
            var stats = new StatistiqueGenresMusicauxVM() { Id = 99, Nom ="test", EstDisponible = true, NbChansonsPubliees=10,NbArtistes=23,NbAbonnees=21 };

            _genreMusicalService_Mock.Setup(g => g.ObtenirParIdAsync(It.IsAny<int>())).ReturnsAsync(genreMusical);
            _genreMusicalService_Mock.Setup(G=>G.StatistiquesUnGenreMusicalAsync(It.IsAny<GenreMusical>())).ReturnsAsync(stats);
            //act
            var result = _genreMusicalController.DetailParID(1).Result;

            _genreMusicalService_Mock.Verify(g=>g.ObtenirParIdAsync(It.IsAny<int>()), Times.Once());

        
            var viewResult = result as ViewResult;
            Assert.NotNull(viewResult);
            Assert.Equal("Details", viewResult.ViewName);
            Assert.IsType<StatistiqueGenresMusicauxVM> (viewResult.Model);
            var model = viewResult.Model as StatistiqueGenresMusicauxVM;
            Assert.Equal(99, model.Id);
        }
        [Fact]
        public async void DetailParId_ObtenirParIdAsync_NotFound()
        {
            //arrange
            
            //act
            var result = _genreMusicalController.DetailParID(1).Result;

            _genreMusicalService_Mock.Verify(g => g.ObtenirParIdAsync(It.IsAny<int>()), Times.Once());

            var viewResult = result as ViewResult;
            Assert.NotNull(viewResult);
            Assert.Equal("NotFound", viewResult.ViewName);
            Assert.Null(viewResult.Model);
        }
    }    
}

