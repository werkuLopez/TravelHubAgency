using Azure.Security.KeyVault.Secrets;
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
        private SecretClient secretClient;
        private string apikey;

        public HomeController(TravelhubServices service,
            IMemoryCache cache,
            SecretClient secretClient)
        {
            this.service = service;
            this.cache = cache;

            this.secretClient = secretClient;
            KeyVaultSecret secret = this.secretClient.GetSecret("apiKey");
            this.apikey = secret.Value;
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
            ViewData["publicaciones"] = await this.service.GetAllPublicacionesAsync(1);
            ViewData["paquetes"] = await this.service.GetAllPackagesAsync();
            ViewData["destinos"] = await this.service.GetAllDestinosAsync();
            ViewData["destinos"] = await this.service.GetAllDestinosAsync();
            ViewData["vuelos"] = await this.service.GetAllVuelosAsync();
            ViewData["hoteles"] = await this.service.GetAllHotelesAsync();
            ViewData["usuarios"] = await this.service.GetAllUsuariosAsync();

            this.cache.Set("APIKEY", this.apikey);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
