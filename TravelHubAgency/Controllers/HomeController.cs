using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using TravelHubAgency.Models;
using TravelHubAgency.Repositories;

namespace TravelHubAgency.Controllers
{
    public class HomeController : Controller
    {
        private TravelhubServices service;
        private IMemoryCache cache;
        public HomeController(TravelhubServices service,
            IMemoryCache cache)
        {
            this.service = service;
            this.cache = cache;
        }

        public async Task<IActionResult> Index()
        {
            List<Continente> continentes;
            if (this.cache.Get("continentes") != null)
            {
                continentes = this.cache.Get<List<Continente>>("continentes");
            }
            else
            {
                continentes = await this.service.GetAllContinentesAsync();
                this.cache.Set("continentes", continentes);
            }

            List<Pais> paises;
            if (this.cache.Get("paises") != null)
            {
                paises = this.cache.Get<List<Pais>>("paises");
            }
            else
            {
                paises = await this.service.GetAllPaisesAsync();
                this.cache.Set("paises", paises);
            }

            ViewData["continentes"] = continentes;
            ViewData["paises"] = paises;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
