using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using TravelHubAgency.Filters;
using TravelHubAgency.Models;
using TravelHubAgency.Repositories;

namespace TravelHubAgency.Controllers
{
    public class BlogsController : Controller
    {
        private TravelhubServices service;

        public BlogsController(TravelhubServices service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Post> publicaciones =
                await this.service.GetAllPublicacionesAsync();

            return View(publicaciones);
        }

        public async Task<IActionResult> SingleBlog(int idpost)
        {
            PostComentariosModel model =
                await this.service.GetPostComentariosModelAsync(idpost);
            return View(model);
        }

        [AuthorizeUsuario]
        [HttpPost]
        public async Task<IActionResult> SingleBlog(string contenido, int idpost)
        {
            Comentario comment =
                await this.service.PublicarComentarioAsync(idpost, contenido);

            //return RedirectToAction("SingleBlog", new { idpost = comment.IdPost });
            PostComentariosModel model =
                await this.service.GetPostComentariosModelAsync(idpost);
            return View(model);
        }
    }
}
