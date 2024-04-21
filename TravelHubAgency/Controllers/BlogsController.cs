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

        //ejemplo
        List<string> estiquetas;

        public BlogsController(TravelhubServices service,
            HelperUploadFiles uploadFiles)
        {
            this.service = service;
            this.uploadFiles = uploadFiles;

            // ejemplo
            this.estiquetas = new List<string>();
            this.estiquetas.Add("Madrid");
            this.estiquetas.Add("Valencia");
            this.estiquetas.Add("Montaña");
            this.estiquetas.Add("Playa");
            this.estiquetas.Add("All");
        }

        public async Task<IActionResult> Index()
        {
            //await this.service.GetAllEtiquetasAsync();
            ViewData["ETIQUETAS"] = this.estiquetas;
            ViewData["actualPage"] = 1;

            return View();
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
            ViewData["ETIQUETAS"] = this.estiquetas;
            ViewData["actualPage"] = page.Value;
            return PartialView("_Publicaciones", publicaciones);
        }

        public async Task<IActionResult> _SingleBlog(int idpost)
        {
            PostComentariosModel model =
                await this.service.GetPostComentariosModelAsync(idpost);
            ViewData["actualPage"] = 1;
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
            return PartialView("_SingleBlog", model);
        }

        [AuthorizeUsuario]
        public async Task<IActionResult> _PublicrPost()
        {
            return PartialView("_PublicrPost");
        }

        [AuthorizeUsuario]
        [HttpPost]
        public async Task<IActionResult> _PublicarPost(Post post, IFormFile file)
        {
            if (file != null)
            {
                await this.uploadFiles.UploadFileAsync(file, Foldders.Images);
                await this.service.PublicarPostAsync(post, file.FileName);

                return RedirectToAction("Index");
            }
            else
            {
                ViewData["mensaje"] = "Debe asignar alguna imagen para su post";
                return PartialView();
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
