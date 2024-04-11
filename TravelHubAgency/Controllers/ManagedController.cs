using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using TravelHubAgency.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TravelHubAgency.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace TravelHubAgency.Controllers
{
    public class ManagedController : Controller
    {
        private IMemoryCache cache;
        private TravelhubServices service;

        public ManagedController(TravelhubServices service, IMemoryCache cache)
        {
            this.service= service;
            this.cache = cache;

        }
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string email, string password)
        {
            Usuario usuario = await this.service.SigIn(email, password);
            if (usuario == null)
            {
                ViewData["MENSAJE"] = "Error al registrarse";
                return View();
            }
            else
            {
                ClaimsIdentity identity =
                    new ClaimsIdentity(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        ClaimTypes.Name, ClaimTypes.Role);

                Claim name = new Claim(ClaimTypes.Name, usuario.Email);
                identity.AddClaim(name);
                Claim id = new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString());
                identity.AddClaim(id);

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal);

                // recuperamos la ruta a la navegacion que el 
                // usuario había seleccionado

                string controller = TempData["controller"].ToString();
                string action = TempData["action"].ToString();

                return RedirectToAction(action, controller);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(string nombre, string apellido, string email, string password,string pais)
        {
            Usuario usuario = await this.service.SigUp(nombre, apellido, email, password, pais);
            if (usuario == null)
            {
                ViewData["MENSAJE"] = "Error al registrarse";
                return View();
            }
            else
            {
                return View(usuario);
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


        public IActionResult ErrorAcceso()
        {
            return View();
        }
    }
}
