using Microsoft.AspNetCore.Mvc;
using TravelHubAgency.Filters;

namespace TravelHubAgency.Controllers
{
    public class UsuariosController : Controller
    {
        [AuthorizeUsuario]
        public IActionResult Perfil()
        {
            return View();
        }
    }
}
