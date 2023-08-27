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
using Microsoft.AspNetCore.Authorization;

namespace TP_FusionVox.Controllers
{

    public class HomeController : Controller
    {

        private readonly IStringLocalizer<HomeController> _localizer;
        private IGenreMusicalService _serviceGM { get; set; }
        private IAgentService _serivceAgent { get; set; }

        public HomeController(IGenreMusicalService serviceGM,
                                IAgentService serviceAgent,
                                IStringLocalizer<HomeController> localizer)
        {
            _serviceGM = serviceGM;
            _serivceAgent = serviceAgent;
            _localizer = localizer;
        }
        [AllowAnonymous]
        [Route("")]
        [Route("Home")]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = this._localizer["IndexTitle"];
            return View(await _serviceGM.StatistiquesTousGenresMusicauxAsync());
        }

        [Authorize]
        [Route("Privacy")]
        [Route("Confidentialite")]
        public IActionResult Privacy()
        {
            ViewData["Title"] = this._localizer["PrivacyTitle"];
            return View();
        }
        [Authorize(Roles = AppConstants.AdminRole + "," + AppConstants.AgentRole)]
        [Route("Dashboard")]
        [Route("TableauDeBord")]
        public async Task<IActionResult> Dashboard()
        {
            var statsDashboard = await _serviceGM.StatistiquesTousGenresMusicauxAsync();
            statsDashboard.StatistiqueAgents = await _serivceAgent.StatistiqueTousAgentAsync();
           
            ViewData["Title"] = this._localizer["DashboarTitle"];
            return View(statsDashboard);
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
