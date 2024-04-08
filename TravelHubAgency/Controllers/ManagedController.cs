using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TravelHubAgency.Models;

namespace TravelHubAgency.Controllers
{
    public class ManagedController : Controller
    {
        private IMemoryCache cache;

        public ManagedController(IMemoryCache cache)
        {
            this.cache = cache;
        }
        public IActionResult Auth()
        {
            //if (cache.Get<string>("usuario") != null)
            //{
            //    ViewData["usuario"] = cache.Get<string>("usuario");
            //    return View();
            //}
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string email, string password)
        {
            string usuario = email + "y su contraseña es " + password;
            this.cache.Set("usuario", usuario);

            return Redirect("Auth");
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(string username, string email, string password)
        {
            string usuario =
                username + ", su correo es " + email + "y su contraseña es " + password;
            this.cache.Set("usuario", usuario);


            return Redirect("Auth");
        }
    }
}
