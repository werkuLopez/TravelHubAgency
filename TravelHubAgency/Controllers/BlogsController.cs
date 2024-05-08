using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using TravelHubAgency.Filters;
using TravelHubAgency.Helpers;
using TravelHubAgency.Models;
using TravelHubAgency.Repositories;

namespace TravelHubAgency.Controllers
{
    public class BlogsController : Controller
    {
        private TravelhubServices service;
        private HelperUploadFiles uploadFiles;


        public BlogsController(TravelhubServices service,
            HelperUploadFiles uploadFiles)
        {
            this.service = service;
            this.uploadFiles = uploadFiles;
        }

        public async Task<IActionResult> Index()
        {
            //await this.service.GetAllEtiquetasAsync();
            List<Etiqueta> etiquetas = await this.service.GetAllEtiquetasAsync();
            return View(etiquetas);
        }

        public async Task<IActionResult> _Publicaciones(int? page)
        {
            if (page == null || page == 0)
            {
                page = 1;
            }

            List<Post> publicaciones =
                await this.service.GetAllPublicacionesAsync(page.Value);
            int numRegistros = publicaciones.Count;

            ViewData["numRegistros"] = numRegistros;
            ViewData["actualPage"] = page.Value;
            ViewData["usuarios"] = await this.service.GetAllUsuariosAsync();
            return PartialView("_Publicaciones", publicaciones);
        }

        public async Task<IActionResult> _SingleBlog(int idpost)
        {
            PostComentariosModel model =
                await this.service.GetPostComentariosModelAsync(idpost);
            ViewData["actualPage"] = 1;
            ViewData["usuarios"] = await this.service.GetAllUsuariosAsync();
            return PartialView("_SingleBlog", model);
        }

        [AuthorizeUsuario]
        [HttpPost]
        public async Task<IActionResult> _SingleBlog(string contenido, int idpost)
        {
            Comentario comment =
                await this.service.PublicarComentarioAsync(idpost, contenido);

            //return RedirectToAction("SingleBlog", new { idpost = comment.IdPost });
            PostComentariosModel model =
                await this.service.GetPostComentariosModelAsync(idpost);
            ViewData["usuarios"] = await this.service.GetAllUsuariosAsync();
            return PartialView("_SingleBlog", model.Post.IdPublicacion);
        }

        [AuthorizeUsuario]
        public async Task<IActionResult> PublicarPost()
        {
            return View();
        }

        [AuthorizeUsuario]
        [HttpPost]
        public async Task<IActionResult> PublicarPost(string contenido, string titulo, IFormFile file)
        {
            Post post = new Post
            {
                Contenido = contenido,
                Titulo = titulo
            };

            if (file != null)
            {
                //await this.uploadFiles.UploadFileAsync(file, Foldders.Images);
                await this.service.PublicarPostAsync(post, file.FileName, file);

                return RedirectToAction("Index");
            }
            else
            {
                ViewData["mensaje"] = "Debe asignar alguna imagen para su post";
                return View();
            }
        }

        [AuthorizeUsuario]
        public async Task<IActionResult> EliminarPost(int idpost)
        {
            await this.service.EliminarPostAsync(idpost);
            return RedirectToAction("Index");
        }
    }
}
