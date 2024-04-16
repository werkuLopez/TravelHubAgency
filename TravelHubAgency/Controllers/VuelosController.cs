using Microsoft.AspNetCore.Mvc;

namespace TravelHubAgency.Controllers
{
    public class VuelosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
