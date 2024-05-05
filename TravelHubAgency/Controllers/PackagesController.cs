using Microsoft.AspNetCore.Mvc;
using TravelHubAgency.Filters;
using TravelHubAgency.Helpers;
using TravelHubAgency.Models;
using TravelHubAgency.Repositories;

namespace TravelHubAgency.Controllers
{
    public class PackagesController : Controller
    {
        private TravelhubServices service;
        private HelperUploadFiles uploadFiles;

        public PackagesController(TravelhubServices service, HelperUploadFiles uploadFiles)
        {
            this.service = service;
            this.uploadFiles = uploadFiles;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["destinos"] = await this.service.GetAllDestinosAsync();

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
            ViewData["vuelos"] = await this.service.GetAllVuelosAsync();
            ViewData["hoteles"] = await this.service.GetAllHotelesAsync();
            return PartialView("_Paquetes", packages);
        }

        public async Task<IActionResult> _SinglePack(int id)
        {
            Package package =
                await this.service.GetPackageByIdAsync(id);
            ViewData["destinos"] = await this.service.GetAllDestinosAsync();
            ViewData["vuelos"] = await this.service.GetAllVuelosAsync();
            ViewData["hoteles"] = await this.service.GetAllHotelesAsync();
            return PartialView("_SinglePack", package);
        }

        public async Task<IActionResult> ReservarPack()
        {
            return View("Index");
        }

        [AuthorizeUsuario]
        [HttpPost]
        public async Task<IActionResult> ReservarPack(int idPaquete)
        {

            ReservaPaquete reserva =
                await this.service.ReservarPaqueteAsync(idPaquete);

            return RedirectToAction("Index");
        }

        [AuthorizeUsuario(Policy = "Administrador")]
        public async Task<IActionResult> CrearPack()
        {
            ViewData["destinos"] = await this.service.GetAllDestinosAsync();
            ViewData["vuelos"] = await this.service.GetAllVuelosAsync();
            ViewData["hoteles"] = await this.service.GetAllHotelesAsync();
            return View();
        }

        [AuthorizeUsuario(Policy = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> CrearPack(Package package, IFormFile file)
        {

            if (file != null)
            {
                this.uploadFiles.UploadFileAsync(file, Foldders.Images);

                package.Imagen = file.FileName;
                Package pack = await this.service.InsertarPackageAsync(package);

                return RedirectToAction("Index");
            }
            else
            {
                ViewData["destinos"] = await this.service.GetAllDestinosAsync();
                ViewData["vuelos"] = await this.service.GetAllVuelosAsync();
                ViewData["hoteles"] = await this.service.GetAllHotelesAsync();

                ViewData["mensaje"] = "Debe asignar una imagen";

                return View();
            }


        }


    }
}
