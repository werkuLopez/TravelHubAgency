﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Packs(string? destino)
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

            return View(packages);
        }

        public async Task<IActionResult> SinglePack(int id)
        {
            Package package =
                await this.service.GetPackageByIdAsync(id);
            return View(package);
        }
    }
}
