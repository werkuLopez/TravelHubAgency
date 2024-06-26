﻿using Microsoft.AspNetCore.Mvc;
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

            Usuario usuario = await this.service.GetPerfilUsuarioAsync();
            if (usuario != null)
            {
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
                usuario = await this.service.UpdateFotoPerfilUsuarioAsync(file);
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

        [AuthorizeUsuario]
        public async Task<IActionResult> UpdatePost(int idpost)
        {
            Post post = await this.service.GetPostByIdAsync(idpost);
            return View(post);
        }

        [AuthorizeUsuario]
        [HttpPost]
        public async Task<IActionResult> UpdatePost(Post post, IFormFile file)
        {
            Post updated = await this.service.UpdatePostAsync(post, file);

            return RedirectToAction("Publicaciones");
        }

        [AuthorizeUsuario]
        [HttpGet]
        public async Task<IActionResult> EliminarPost(int idpost)
        {
            await this.service.EliminarPostAsync(idpost);

            return RedirectToAction("Publicaciones");
        }
    }
}
