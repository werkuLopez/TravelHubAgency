using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using TravelHubAgency.Models;
using TravelHubAgency.Repositories;

namespace TravelHubAgency.Controllers
{
    public class HomeController : Controller
    {
        private ITravelhubRepository repo;
        private IMemoryCache cache;
        public HomeController(ITravelhubRepository repo,
            IMemoryCache cache)
        {
            this.repo = repo;
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
                continentes = await this.repo.GetAllContinentesAsync();
                this.cache.Set("continentes", continentes);
            }

            List<Pais> paises;
            if (this.cache.Get("paises") != null)
            {
                paises = this.cache.Get<List<Pais>>("paises");
            }
            else
            {
                paises = await this.repo.GetAllPaisesAsync();
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
