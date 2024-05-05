using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelHubAgency.Filters;
using TravelHubAgency.Models;
using TravelHubAgency.Repositories;

namespace TravelHubAgency.Controllers
{
    public class UsuariosController : Controller
    {
        private TravelhubServices service;

        public UsuariosController(TravelhubServices service)
        {
            this.service = service;
        }

        [AuthorizeUsuario]
        public async Task<IActionResult> Perfil()
        {
            string claimId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int idusuario = int.Parse(claimId);

            if (idusuario != null)
            {
                Usuario usuario = await this.service.GetUsuarioByIdAsync(idusuario);
                //usuario = await this.service.GetPerfilUsuarioAsync();
                return View(usuario);
            }

            return RedirectToAction("LogOut", "Managed");


        }

        [AuthorizeUsuario]
        [HttpPost]
        public async Task<IActionResult> Perfil(string nombre, string apellido, string email, string password, string telefono, string pais, IFormFile? file)
        {
            Usuario usuario;

            usuario =
            await this.service.UpdatePerfilUsuarioAsync(nombre, apellido, email, password, telefono, pais);

            if (file != null)
            {
                usuario = await this.service.UpdateFotoPerfilUsuarioAsync(file.FileName);
            }

            return View(usuario);
        }

        [AuthorizeUsuario]
        public async Task<IActionResult> Publicaciones()
        {

            List<Post> misPublicaciones = await this.service.GetPublicacionesUsuario();
            ViewData["usuarios"] = await this.service.GetAllUsuariosAsync();

            return View(misPublicaciones);
        }
    }
}
