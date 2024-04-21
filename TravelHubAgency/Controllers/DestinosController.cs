using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TravelHubAgency.Filters;
using TravelHubAgency.Models;
using TravelHubAgency.Repositories;

namespace TravelHubAgency.Controllers
{
    public class DestinosController : Controller
    {
        private TravelhubServices service;
        private IMemoryCache cache;

        public DestinosController(TravelhubServices service, IMemoryCache cache)
        {
            this.service = service;
            this.cache = cache;
        }

        public async Task<IActionResult> Index(int? idcontinente)
        {
            List<Destino> destinos;
            if (idcontinente == null)
            {
                destinos = await this.service.GetAllDestinosAsync();
            }
            else
            {
                destinos = await this.service.GetAllDestinosContinenteAsync(idcontinente.Value);

            }

            int numRegistros = destinos.Count;
            ViewData["numRegistros"] = numRegistros;
            ViewData["idcontinente"] = idcontinente;
            ViewData["atualPage"] = 1;

            return View(destinos);
        }
        public async Task<IActionResult> _Destinos(int? idcontinente, int? page)
        {
            if (page == null || page == 0)
            {
                page = 1;
            }

            List<Destino> destinos;
            if (idcontinente == null)
            {
                destinos = await this.service.GetDestinosPaginadosAsync(page.Value);
            }
            else
            {
                destinos = await this.service.GetAllDestinosContinenteAsync(idcontinente.Value);
            }

            ViewData["atualPage"] = page.Value;
            return PartialView(destinos);
        }

        public async Task<IActionResult> _SingleDestino(int iddestino)
        {
            Destino destino = await this.service.GetDestinoByIdAsync(iddestino);
            ViewData["atualPage"] = 1;
            return PartialView("_SingleDestino", destino);
        }

        [AuthorizeUsuario(Policy = "Administrador")]
        [HttpGet]
        public async Task<IActionResult> CrearDestino()
        {
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

            ViewData["paises"] = paises;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearDestino(Destino destino, IFormFile file)
        {

            Destino detino = await this.service.InsertarDestinoAsync(destino.Nombre, destino.IdPais, destino.Region, destino.Descripcion, file.FileName, destino.Latitud, destino.Longitud, destino.Precio);
            return RedirectToAction("Destinos");
        }
    }
}
