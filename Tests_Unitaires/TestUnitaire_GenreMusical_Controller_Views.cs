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
    public class TestUnitaire_GenreMusical_Controller_Views
    {
        private Mock<IGenreMusicalService> _genreMusicalService_Mock;
        private Mock<IArtisteService> _artisteService_Mock;
        private Mock<IWebHostEnvironment> _webHostEnvironment_Mock;
        private Mock<IStringLocalizer<GenreMusicalController>> _stringLocalizerMock;
        private GenreMusicalController _genreMusicalController;

        public TestUnitaire_GenreMusical_Controller_Views()
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
        public async Task DetailParID_ObtenirParIdAsync_ValidID()
        {
            //Arange
            var genreMusicalMock = new GenreMusical()
            {
                Id = 1,
                Nom = "POP",
                ImageUrl = "POP.jpg",
                Description = "La musique pop"
            };

            _genreMusicalService_Mock.Setup(a => a.ObtenirParIdAsync(1)).ReturnsAsync(genreMusicalMock);
            //Act
            var result = await _genreMusicalController.DetailParID(1);

            //Assert
            _genreMusicalService_Mock.Verify(g => g.ObtenirParIdAsync(1), Times.Once());

            ViewResult viewResult = result as ViewResult;

            Assert.Equal("Details", viewResult.ViewName);

            Assert.NotNull(viewResult.ViewData);

            //Assert.Equal(genreMusicalMock, viewResult.Model);

            var model = viewResult.ViewData.Model as GenreMusical;

            Assert.Equal(1, model.Id);

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
            _genreMusicalService_Mock.Setup(g => g.CreerAsync(It.IsAny<GenreMusical>())).ReturnsAsync(new GenreMusical());
            _genreMusicalService_Mock.Verify(g => g.CreerAsync(It.IsAny<GenreMusical>()), Times.Once());
            var result = _genreMusicalController.Upsert(new GenreMusicalVM()).Result;

            

            Assert.IsType<RedirectToActionResult>(result);
            var viewResult = result as RedirectToActionResult;

            Assert.Equal("Index", viewResult.ActionName);
            Assert.Equal("Home", viewResult.ControllerName);
            Assert.Null(viewResult.ControllerName);
        }
    }
    
}

