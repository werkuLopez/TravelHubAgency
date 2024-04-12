using Microsoft.AspNetCore.Mvc;

namespace TravelHubAgency.Controllers
{
    public class PackagesController : Controller
    {
        public IActionResult Packs()
        {
            return View();
        }
    }
}
