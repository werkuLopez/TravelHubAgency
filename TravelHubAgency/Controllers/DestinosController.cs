using Microsoft.AspNetCore.Mvc;
using TravelHubAgency.Models;
using TravelHubAgency.Repositories;

namespace TravelHubAgency.Controllers
{
    public class DestinosController : Controller
    {
        private TravelhubServices service;

        public DestinosController(TravelhubServices service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Destinos()
        {
            List<Destino> destinos = await this.service.GetAllDestinosAsync();
            return View(destinos);
        }

        public async Task<IActionResult> SingleDestino(int id)
        {
            return View();
        }
    }
}
