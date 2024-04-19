﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Destinos(int? idcontinente, int? page)
        {
            if (page == null || page == 0)
            {
                page = 1;
            }


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

            return View(destinos);
        }

        public async Task<IActionResult> SingleDestino(int id)
        {
            Destino destino = await this.service.GetDestinoByIdAsync(id);
            return View(destino);
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
