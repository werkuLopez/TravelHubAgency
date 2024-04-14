using Microsoft.AspNetCore.Mvc;
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
            Usuario usuario = await this.service.GetPerfilUsuarioAsync();

            return View(usuario);
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

        //[AuthorizeUsuario]
        //[HttpPost]
        //public async Task<IActionResult> Perfil(IFormFile file)
        //{
        //    ///Usuario usuario = await this.service
        //    return View(new Usuario());
        //}
    }
}
