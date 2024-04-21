using Microsoft.AspNetCore.Mvc;
using TravelHubAgency.Filters;
using TravelHubAgency.Models;
using TravelHubAgency.Repositories;

namespace TravelHubAgency.Controllers
{
    public class VuelosController : Controller
    {
        private TravelhubServices service;

        public VuelosController(TravelhubServices service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {

            return View();
        }

        public async Task<IActionResult> _Vuelos()
        {

            List<Vuelo> vuelos =
                await this.service.GetAllVuelosAsync();

            ViewData["DESTINOS"] = await this.service.GetAllDestinosAsync();

            return PartialView("_Vuelos", vuelos);
        }

        public async Task<IActionResult> _DetalleVuelo(int idvuelo)
        {
            Vuelo vuelo = await this.service.GetVueloByIdAsync(idvuelo);
            ViewData["DESTINOS"] = await this.service.GetAllDestinosAsync();
            return PartialView("_DetalleVuelo", vuelo);
        }

        [AuthorizeUsuario(Policy = "Administrador")]
        public async Task<IActionResult> CrearVuelo()
        {
            ViewData["DESTINOS"] = await this.service.GetAllDestinosAsync();
            return View();
        }

        [AuthorizeUsuario]
        [HttpPost]
        public async Task<IActionResult> CrearVuelo(Vuelo vuelo, string latitud, string longitud)
        {
            ViewData["DESTINOS"] = await this.service.GetAllDestinosAsync();
            Vuelo newVuelo =
                await this.service.CrearVueloAsync(vuelo, latitud, longitud);
            return View();

        }
        [AuthorizeUsuario]
        [HttpGet]
        public async Task<IActionResult> _UpdateVuelo(int idvuelo)
        {
            Vuelo vuelo = await this.service.GetVueloByIdAsync(idvuelo);
            ViewData["DESTINOS"] = await this.service.GetAllDestinosAsync();
            return PartialView("_UpdateVuelo", vuelo);
        }
        [AuthorizeUsuario]
        [HttpPost]
        public async Task<IActionResult> _UpdateVuelo(Vuelo vuelo, string latitud, string longitud)
        {
            Vuelo updated = await this.service.UpdateVueloAsync(vuelo, latitud, longitud);
            return RedirectToAction("Index");
        }

        [AuthorizeUsuario(Policy = "Administrador")]
        [HttpDelete]
        public async Task<IActionResult> Eliminar(int idvuelo)
        {
            await this.service.EliminarVueloAsync(idvuelo);
            return RedirectToAction("_Vuelos");
        }


        public async Task<IActionResult> _ComrprarVuelo(int idvuelo)
        {
            Vuelo vuelo = await this.service.GetVueloByIdAsync(idvuelo);
            return PartialView("_ComprarVuelo", vuelo);
        }

        [AuthorizeUsuario]
        [HttpPost]
        public async Task<IActionResult> _ComprarVuelo(int idvuelo)
        {
            ReservaVuelo reserva = await this.service.ComprarVueloAsync(idvuelo);
            return PartialView();
            //return RedirectToAction("Index", "Reservas");
        }
    }
}
