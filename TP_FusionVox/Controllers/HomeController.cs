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

        private readonly IStringLocalizer<HomeController> _localizer;
        private IGenreMusicalService _serviceGM { get; set; }

        public HomeController(IGenreMusicalService serviceGM,
                                IStringLocalizer<HomeController> localizer)
        {
            _serviceGM = serviceGM;
            _localizer = localizer;
        }

        [Route("")]
        [Route("Home")]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = this._localizer["IndexTitle"];
            return View(await _serviceGM.StatistiquesTousGenresMusicauxAsync());
        }
        [Route("Privacy")]
        [Route("Confidentialite")]
        public IActionResult Privacy()
        {
            ViewData["Title"] = this._localizer["PrivacyTitle"];
            return View();
        }

        //[Route("Dashboard")]
        //[Route("TableauDeBord")]
        //public async Task<IActionResult> Dashboard()
        //{
        //    ViewData["Title"] = this._localizer["DashboarTitle"];
        //    return View(await _serviceGM.StatistiquesTousGenresMusicauxAsync());
        //}

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
