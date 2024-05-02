using Microsoft.AspNetCore.Mvc;
using TravelHubAgency.Filters;
using TravelHubAgency.Models;
using TravelHubAgency.Repositories;

namespace TravelHubAgency.Controllers
{
    public class PackagesController : Controller
    {
        private TravelhubServices service;

        public PackagesController(TravelhubServices service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> _Paquetes(string? destino)
        {
            List<Package> packages;

            if (destino == null)
            {
                packages
                     = await this.service.GetAllPackagesAsync();
            }
            else
            {
                packages =
                    await this.service.GetPackagesByDestinoAsync(destino);
            }

            ViewData["destino"] = destino;
            ViewData["destinos"] = await this.service.GetAllDestinosAsync();
            return PartialView("_Paquetes", packages);
        }

        public async Task<IActionResult> _SinglePack(int id)
        {
            Package package =
                await this.service.GetPackageByIdAsync(id);
            ViewData["destinos"] = await this.service.GetAllDestinosAsync();
            return PartialView("_SinglePack", package);
        }

        public async Task<IActionResult> _ReservarPack()
        {
            return PartialView("_ReservarPaquete");
        }

        [AuthorizeUsuario]
        [HttpPost]
        public async Task<IActionResult> _ReservarPack(ReservaPaquete paquete)
        {
            ReservaPaquete reserva =
                await this.service.ReservarPaqueteAsync(paquete);

            return PartialView("_Paquetes");
        }


        [AuthorizeUsuario(Policy = "Administrador")]
        public async Task<IActionResult> CrearPack(Package package, IFormFile file)
        {
            return RedirectToAction("Index");
        }


    }
}
