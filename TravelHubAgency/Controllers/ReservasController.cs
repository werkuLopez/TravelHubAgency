using Microsoft.AspNetCore.Mvc;
using TravelHubAgency.Filters;
using TravelHubAgency.Models;
using TravelHubAgency.Repositories;

namespace TravelHubAgency.Controllers
{
    public class ReservasController : Controller
    {
        private TravelhubServices service;

        public ReservasController(TravelhubServices service)
        {
            this.service = service;
        }

        [AuthorizeUsuario]
        public async Task<IActionResult> ReservasUsuario()
        {
            ReservaModel reservas = await this.service.ReservasUsuarioAsync();
            ViewData["estadoReserva"] = await this.service.GetEstadoReservaAsync();
            ViewData["destinos"] = await this.service.GetAllDestinosAsync();
            ViewData["paquetes"] = await this.service.GetAllPackagesAsync();
            ViewData["vuelos"] = await this.service.GetAllVuelosAsync();
            ViewData["hoteles"] = await this.service.GetAllHotelesAsync();
            return View(reservas);
        }

        [AuthorizeUsuario]
        [HttpPost]
        public async Task<IActionResult> EliminarReservaPack(int id)
        {
            await this.service.EliminarReservaPaquete(id);
            return RedirectToAction("ReservasUsuario");
        }

        [AuthorizeUsuario]
        [HttpPost]
        public async Task<IActionResult> EliminarReservaDestino(int id)
        {
            await this.service.EliminarReservaDestino(id);
            return RedirectToAction("ReservasUsuario");
        }

        [AuthorizeUsuario]
        [HttpPost]
        public async Task<IActionResult> EliminarReservaVuelo(int id)
        {
            await this.service.EliminarReservaVuelo(id);
            return RedirectToAction("ReservasUsuario");
        }

        [AuthorizeUsuario]
        [HttpPost]
        public async Task<IActionResult> EliminarReservaHotel(int id)
        {
            await this.service.EliminarReservaHotelAsync(id);
            return RedirectToAction("ReservasUsuario");
        }
    }
}
