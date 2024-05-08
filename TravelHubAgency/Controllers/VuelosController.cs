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

        //[HttpGet]
        //public async Task<IActionResult> _ComprarVuelo(int idvuelo)
        //{
        //    Vuelo vuelo = await this.service.GetVueloByIdAsync(idvuelo);
        //    ViewData["DESTINOS"] = await this.service.GetAllDestinosAsync();
        //    return PartialView("_ComprarVuelo", vuelo);
        //}

        //[AuthorizeUsuario]
        //[HttpPost]
        //public async Task<IActionResult> _ComprarVuelo(PagoVuelo pago)
        //{
        //    ReservaVuelo reserva = await this.service.ComprarVueloAsync(pago.IdVuelo, pago);
        //    ViewData["DESTINOS"] = await this.service.GetAllDestinosAsync();
        //    return PartialView();
        //}

        [AuthorizeUsuario]
        [HttpGet]
        public async Task<IActionResult> ComprarVuelo(int idvuelo)
        {
            //if (HttpContext.Session.GetString("idvuelo") != null)
            //{
            //    idvuelo = int.Parse(HttpContext.Session.GetString("idvuelo"));
            //}
            //else
            //{
            //    HttpContext.Session.SetString("idvuelo", idvuelo.ToString());
            //}

            Vuelo vuelo = await this.service.GetVueloByIdAsync(idvuelo);
            ViewData["DESTINOS"] = await this.service.GetAllDestinosAsync();
            return View(vuelo);
        }

        [AuthorizeUsuario]
        [HttpPost]
        public async Task<IActionResult> ComprarVuelo(int idvuelo, string metodopago, int precio)
        {
            PagoVuelo pago = new PagoVuelo
            {
                IdVuelo = idvuelo,
                MetodoPago = metodopago,
                Precio = precio
            };

            ReservaVuelo reserva = await this.service.ComprarVueloAsync(pago.IdVuelo, pago);
            ViewData["DESTINOS"] = await this.service.GetAllDestinosAsync();
            return RedirectToAction("ReservasUsuario", "Reservas");
        }

        [AuthorizeUsuario]
        [HttpDelete]
        public async Task<IActionResult> EliminarReserva(int idreservavuelo)
        {
            await this.service.EliminarReservaVuelo(idreservavuelo);

            return RedirectToAction("ReservasUsuario", "Reservas");
        }
    }
}
