using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using TravelHubAgency.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TravelHubAgency.Repositories;
using static System.Net.Mime.MediaTypeNames;
using System.Numerics;

namespace TravelHubAgency.Controllers
{
    public class ManagedController : Controller
    {
        private IMemoryCache cache;
        private TravelhubServices service;

        public ManagedController(TravelhubServices service, IMemoryCache cache)
        {
            this.service = service;
            this.cache = cache;

        }
        public IActionResult LogIn()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginModel model)
        {
            //Usuario usuario = await this.service.SigIn(model.Email, model.Password);
            string token = await this.service.GetTokenAsync(model.Email, model.Password);
            if (token == null)
            {
                ViewData["MENSAJE"] = "Error al registrarse";
                return View();
            }
            else
            {
                Usuario usuario = await this.service.GetUsuarioByUsername(model.Email);

                ClaimsIdentity identity =
                    new ClaimsIdentity(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        ClaimTypes.Name, ClaimTypes.Role);

                Claim name = new Claim(ClaimTypes.Name, model.Email);
                identity.AddClaim(name);
                //Claim claimId = new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString());
                //identity.AddClaim(claimId);

                // añadimos el token al clams
                //identity.AddClaim(new Claim("Token", token));
                Claim tokenClaim = new Claim("TOKEN", token);
                identity.AddClaim(tokenClaim);

                if (usuario.TipoUsuario == 1)
                {
                    //creamos su propio claimId
                   identity.AddClaim(new Claim("Administrador", usuario.TipoUsuario.ToString()));
                   identity.AddClaim(new Claim(ClaimTypes.Role, "Administrador"));
                }

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal, new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
                    });

                // recuperamos la ruta a la navegacion que el 
                // usuario había seleccionado

                string controller = TempData["controller"].ToString();
                string action = TempData["action"].ToString();

                return RedirectToAction(action, controller);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterModel model)
        {
            RegisterModel exist = await this.service.SigUp(model.Nombre, model.Apellido, model.Email, model.Password, model.Telefono, model.Pais);
            if (exist == null)
            {
                ViewData["MENSAJE"] = "Error al registrarse";
            }
            return View();

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
