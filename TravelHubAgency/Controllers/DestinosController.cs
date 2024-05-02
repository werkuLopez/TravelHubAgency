using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TravelHubAgency.Filters;
using TravelHubAgency.Helpers;
using TravelHubAgency.Models;
using TravelHubAgency.Repositories;

namespace TravelHubAgency.Controllers
{
    public class DestinosController : Controller
    {
        private TravelhubServices service;
        private IMemoryCache cache;
        private HelperUploadFiles uploadFiles;


        public DestinosController(
            TravelhubServices service,
            IMemoryCache cache,
            HelperUploadFiles uploadFiles)
        {
            this.service = service;
            this.cache = cache;
            this.uploadFiles = uploadFiles;
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

            //int numRegistros = destinos.Count;
            //ViewData["numRegistros"] = numRegistros;
            //ViewData["idcontinente"] = idcontinente;
            //ViewData["atualPage"] = 1;
            return View(destinos);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string destinosearched)
        {
            List<Destino> destinos = await this.service.GetDestinoByNameAsync(destinosearched);

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

            int numRegistros = destinos.Count;
            ViewData["numRegistros"] = numRegistros;
            ViewData["idcontinente"] = idcontinente;
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
            if (file != null)
            {
                this.uploadFiles.UploadFileAsync(file, Foldders.Images);

                Destino detino =
                    await this.service.InsertarDestinoAsync(
                        destino.Nombre, destino.IdPais,
                    destino.Region, destino.Descripcion, file.FileName, destino.Latitud,
                    destino.Longitud, destino.Precio);

                return RedirectToAction("Destinos");
            }
            else
            {
                ViewData["mensaje"] = "Debe asignar una imagen";
                return View();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDestino(Destino destino, IFormFile file)
        {
            if (file != null)
            {
                this.uploadFiles.UploadFileAsync(file, Foldders.Images);

                await this.service.UpdateDestinoAsync(destino.IdDestino, destino.Nombre, destino.IdPais,
                    destino.Region, destino.Descripcion, file.FileName, destino.Latitud,
                    destino.Longitud, destino.Precio);

                return RedirectToAction("Destinos");
            }
            else
            {
                await this.service.UpdateDestinoAsync(destino.IdDestino, destino.Nombre, destino.IdPais,
                    destino.Region, destino.Descripcion, file.FileName, destino.Latitud,
                    destino.Longitud, destino.Precio);
                return RedirectToAction("Destinos");
            }
        }

        public async Task<IActionResult> EliminarDestino(int iddestino)
        {
            await this.service.EliminarDestino(iddestino);
            return RedirectToAction("Destinos");
        }
    }
}
