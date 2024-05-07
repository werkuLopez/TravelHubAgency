using Microsoft.AspNetCore.Mvc;
using TravelHubAgency.Filters;
using TravelHubAgency.Helpers;
using TravelHubAgency.Models;
using TravelHubAgency.Repositories;

namespace TravelHubAgency.Controllers
{
    public class HotelesController : Controller
    {
        private TravelhubServices service;
        private HelperUploadFiles uploadFiles;
        public HotelesController(TravelhubServices service, HelperUploadFiles uploadFiles)
        {
            this.service = service;
            this.uploadFiles = uploadFiles;
        }


        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> _Hoteles()
        {
            List<Hotel> hoteles =
            await this.service.GetAllHotelesAsync();

            return PartialView("_Hoteles", hoteles);
        }

        public async Task<IActionResult> _SingleHotel(int id)
        {
            Hotel hotel =
                await this.service.GetHotelByIdAsync(id);

            return PartialView("_SingleHotel", hotel);
        }
        [AuthorizeUsuario]
        public IActionResult CrearHotel()
        {
            return View();
        }

        [AuthorizeUsuario(Policy = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> CrearHotel(Hotel hotel, IFormFile file)
        {
            if (file != null)
            {
                //this.uploadFiles.UploadFileAsync(file, Foldders.Images);

                hotel.Imagen = file.FileName;
                Hotel newHotel =
                await this.service.InsertarHotelAsync(hotel, file);

                return RedirectToAction("Index");
            }
            else
            {
                ViewData["mensaje"] = "Debe asignar una imagen";

                return View();
            }
        }

        [AuthorizeUsuario(Policy = "Administrador")]
        public async Task<IActionResult> ELiminarHotel(int idhotel)
        {
            await this.service.ELiminarHotelAsync(idhotel);

            return RedirectToAction("Index");
        }

        [AuthorizeUsuario]
        public async Task<IActionResult> ReservarHotel(int id)
        {
            //if (HttpContext.Session.GetString("idhotel") != null)
            //{
            //    id = int.Parse(HttpContext.Session.GetString("idhotel"));
            //}
            //else
            //{
            //    HttpContext.Session.SetString("idhotel", id.ToString());
            //}

            Hotel hotel = await this.service.GetHotelByIdAsync(id);
            return View(hotel);
        }

        [AuthorizeUsuario]
        [HttpPost]
        public async Task<IActionResult> ReservarHotel(int idhotel, int personas, int precio, DateTime fecha)
        {
            ReservaHotel reserva = new ReservaHotel
            {
                Fecha = fecha,
                Hotel = idhotel,
                Personas = personas,
                Precio = precio
            };

            reserva = await this.service.InsertarReservaHotelAsync(reserva);

            Hotel hotel = await this.service.GetHotelByIdAsync(reserva.Hotel);
            return RedirectToAction("ReservasUsuario", "Reservas");
        }
    }
}
