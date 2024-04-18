using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using TravelHubAgency.Data;
using TravelHubAgency.Helpers;
using TravelHubAgency.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TravelHubAgency.Repositories
{
    public class TravelhubServices
    {
        private string ApiUrl;
        private MediaTypeWithQualityHeaderValue header;
        private IHttpContextAccessor context;

        public TravelhubServices(IConfiguration config, IHttpContextAccessor context)
        {
            this.ApiUrl = config.GetValue<string>("ApiUrls:ApiTravelHub");
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
            this.context = context;
        }


        #region CALLAPISASYNC
        // sin token
        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);

                //string token = this.context.HttpContext.User.FindFirst(x => x.Type == "Token").Value;
                //if(token != null)
                //{

                //}
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        // con token
        public async Task<T> CallApiAsync<T>(string request, string token)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(this.ApiUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.header);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage response = await client.GetAsync(request);
                    response.EnsureSuccessStatusCode();

                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción aquí
                // Por ejemplo, puedes registrarla o lanzarla nuevamente
                throw ex;
            }
        }

        #endregion

        #region GETUSUARIOTOKEN
        public async Task<string> GetTokenAsync(string username, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/auth/login";
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);

                LoginModel model = new LoginModel
                {
                    Email = username,
                    Password = password
                };

                string jsonData =
                    JsonConvert.SerializeObject(model);
                StringContent content =
                    new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                   await client.PostAsync(request, content);

                if (response.IsSuccessStatusCode)
                {
                    string data =
                        await response.Content.ReadAsStringAsync();
                    JObject keys = JObject.Parse(data);
                    string token = keys.GetValue("response").ToString();
                    return token;
                }
                else
                {
                    return null;
                }
            };
        }

        #endregion

        #region CONTINENTES

        public async Task<List<Continente>> GetAllContinentesAsync()
        {
            string request = "api/continentes/allcontinentes";
            List<Continente> data = await this.CallApiAsync<List<Continente>>(request);
            return data;
        }
        #endregion

        #region PAISES

        public async Task<List<Pais>> GetAllPaisesAsync()
        {
            string request = "api/paises/allpaises";
            List<Pais> data = await this.CallApiAsync<List<Pais>>(request);
            return data;
        }

        public async Task<List<Pais>> GetAllPaisesByContinenteAsync(int contiente)
        {
            string request = "api/paises/paisescontinente/" + contiente;
            List<Pais> data = await this.CallApiAsync<List<Pais>>(request);
            return data;
        }

        #endregion

        #region DESTINOS 

        public async Task<List<Destino>> GetAllDestinosAsync()
        {
            string request = "api/destinos/alldestinos";
            List<Destino> destinos = await this.CallApiAsync<List<Destino>>(request);
            return destinos;
        }

        public async Task<List<Destino>> GetDestinoByNameAsync(string destino)
        {
            string request = "api/destinos/destinos?destino=" + destino;
            List<Destino> destinos = await this.CallApiAsync<List<Destino>>(request);
            return destinos;
        }

        public async Task<List<Destino>> GetAllDestinosContinenteAsync(int idcontinente)
        {
            string request = "/api/destinos/destinoscontinente/" + idcontinente;
            List<Destino> destinos = await this.CallApiAsync<List<Destino>>(request);
            return destinos;
        }

        public async Task<Destino> GetDestinoByIdAsync(int iddestino)
        {
            string request = "api/destinos/finddestino/" + iddestino;
            Destino destinos = await this.CallApiAsync<Destino>(request);
            return destinos;
        }

        public async Task<Destino> InsertarDestinoAsync(string nombre,
            int idpais, string region, string descripcion,
            string imagen, string latitud, string longitud, decimal precio)
        {
            string token =
                this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;
            using (HttpClient client = new HttpClient())
            {
                string request = "api/destinos/insertardestino";

                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                Destino model = new Destino
                {
                    Nombre = nombre,
                    Region = region,
                    IdPais = idpais,
                    Descripcion = descripcion,
                    Longitud = longitud,
                    Latitud = latitud,
                    Imagen = imagen,
                    Precio = precio
                };

                string jsonData =
                    JsonConvert.SerializeObject(model);
                StringContent content =
                    new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                   await client.PostAsync(request, content);

                if (response.IsSuccessStatusCode)
                {
                    Destino data =
                        await response.Content.ReadAsAsync<Destino>();

                    return data;
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        #region PACKAGES 
        public async Task<List<Package>> GetAllPackagesAsync()
        {
            string request = "api/packages/allpackages";
            List<Package> packs = await this.CallApiAsync<List<Package>>(request);
            return packs;
        }

        public async Task<List<Package>> GetPackagesByDestinoAsync(string destino)
        {
            string request = "api/packages/findpackage?destino=" + destino;
            List<Package> packages = await this.CallApiAsync<List<Package>>(request);

            return packages;
        }

        public async Task<Package> GetPackageByIdAsync(int id)
        {
            string request = "api/packages/findpackage/" + id;
            Package package =
                await this.CallApiAsync<Package>(request);

            return package;
        }

        #endregion

        #region USUARIOS

        public async Task<List<Usuario>> GetAllUsuariosAsync()
        {
            string request = "api/usuarios/allusuarios";
            List<Usuario> usuarios =
                await this.CallApiAsync<List<Usuario>>(request);

            return usuarios;
        }

        public async Task<Usuario> GetPerfilUsuarioAsync()
        {
            string token =
              this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;

            string request = "api/usuarios/perfilusuario";

            Usuario usuario =
                await this.CallApiAsync<Usuario>(request, token);

            return usuario;
        }

        public async Task<Usuario> UpdatePerfilUsuarioAsync(string nombre, string apellido, string email, string password, string telefono, string pais)
        {
            string token =
                this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;

            using (HttpClient client = new HttpClient())
            {
                string request = "api/usuarios/updateperfilusuario";
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                RegisterModel model = new RegisterModel
                {
                    Nombre = nombre,
                    Apellido = apellido,
                    Email = email,
                    Password = password,
                    Telefono = telefono,
                    Pais = pais
                };

                if (password == null)
                {
                    model.Password = "";
                }
                string jsonData =
                    JsonConvert.SerializeObject(model);
                StringContent content =
                    new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                   await client.PutAsync(request, content);

                if (response.IsSuccessStatusCode)
                {
                    RegisterModel data =
                        await response.Content.ReadAsAsync<RegisterModel>();

                    List<Usuario> usuarios = await GetAllUsuariosAsync();

                    Usuario updated = usuarios.Where(x => x.Email == model.Email).FirstOrDefault();

                    return updated;
                }
                else
                {
                    return null;
                }
            };
        }

        public async Task<Usuario> GetUsuarioByUsername(string username)
        {
            List<Usuario> usuarios = await this.GetAllUsuariosAsync();

            return usuarios.FirstOrDefault(x => x.Email == username);
        }

        public async Task<Usuario> UpdateFotoPerfilUsuarioAsync(string imagen)
        {
            string token =
    this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;

            string request = "api/usuarios/updatefotoperfilusuario?imagen=" + imagen;

            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response =
                    await client.PutAsync(request, null);

                if (response.IsSuccessStatusCode)
                {
                    Usuario usuario =
                        await response.Content.ReadAsAsync<Usuario>();
                    return usuario;
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        #region POSTSCOMENTARIOSMODEL

        public async Task<PostComentariosModel> GetPostComentariosModelAsync(int idpost)
        {
            PostComentariosModel model = new PostComentariosModel();
            model.Post = await this.GetPostByIdAsync(idpost);
            model.Comentarios = await this.GetComentariosPostAsync(idpost);

            return model;
        }

        #endregion

        #region PUBLICACIONES

        public async Task<List<Post>> GetAllPublicacionesAsync()
        {
            string request = "api/publicaciones/allposts";
            List<Post> posts = await this.CallApiAsync<List<Post>>(request);
            return posts;
        }

        public async Task<List<Post>> GetPostsByUsuarioAsync()
        {
            string token =
                this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;

            string request = "api/publicaciones/postsusuario";
            List<Post> postsUsuario = await this.CallApiAsync<List<Post>>(request, token);

            return postsUsuario;
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            string request = "api/publicaciones/post/" + id;
            Post post =
                await this.CallApiAsync<Post>(request);

            return post;
        }

        public async Task<Post> PublicarPostAsync(Post post, string imagen)
        {
            string token =
            this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;

            using (HttpClient client = new HttpClient())
            {
                string request = "api/publicaciones/crearpost";
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                Post newPost = new Post
                {
                    IdPublicacion = 0,
                    Contenido = post.Contenido,
                    FechaPublicacion = post.FechaPublicacion,
                    Imagen = imagen,
                    Titulo = post.Titulo,

                    IdUsuario = 0, // le paso 0 porque en la
                                   // api me encargo de recuperar el id el
                                   // usuario por el token
                };

                string jsonData =
                    JsonConvert.SerializeObject(newPost);
                StringContent content =
                    new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                   await client.PostAsync(request, content);

                if (response.IsSuccessStatusCode)
                {
                    Post postUloaded =
                        await response.Content.ReadAsAsync<Post>();
                    return postUloaded;
                }
                else
                {
                    return null;
                }

            }

        }

        public async Task EliminarPostAsync(int id)
        {
            string request = "api/publicaciones/EliminarPost/" + id;
            int reult = await this.CallApiAsync<int>(request);
        }

        #endregion

        #region COMENTARIOS

        public async Task<List<Comentario>> GetAllComentariosAsync()
        {
            string request = "api/comentarios/allcomentarios";
            List<Comentario> comentarios =
                await this.CallApiAsync<List<Comentario>>(request);

            return comentarios;
        }

        public async Task<List<Comentario>> GetComentariosPostAsync(int idpost)
        {
            string request = "api/comentarios/comentariospost/" + idpost;
            List<Comentario> comentarios =
                await this.CallApiAsync<List<Comentario>>(request);

            return comentarios;
        }

        public async Task<Comentario> PublicarComentarioAsync(int idpost, string contenido)
        {
            string token =
            this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;

            using (HttpClient client = new HttpClient())
            {
                string request = "api/comentarios/publicarcomentario";
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                Comentario newComent = new Comentario
                {
                    IdComentario = 0,
                    Contenido = contenido,
                    Fecha = DateTime.Now,
                    IdPost = idpost,
                    IdUsuario = 0, // le paso 0 porque en la
                                   // api me encargo de recuperar el id el
                                   // usuario por el token
                };

                string jsonData =
                    JsonConvert.SerializeObject(newComent);
                StringContent content =
                    new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                   await client.PostAsync(request, content);

                if (response.IsSuccessStatusCode)
                {
                    Comentario comentUloaded =
                        await response.Content.ReadAsAsync<Comentario>();
                    return comentUloaded;
                }
                else
                {
                    return null;
                }

            }
        }

        #endregion




        #region AUTH

        public async Task<RegisterModel> SigUp(string nombre, string apellido, string email, string password, string telefono, string pais)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/auth/signup";
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);

                RegisterModel model = new RegisterModel
                {
                    Nombre = nombre,
                    Apellido = apellido,
                    Email = email,
                    Password = password,
                    Telefono = telefono,
                    Pais = pais
                };

                string jsonData =
                    JsonConvert.SerializeObject(model);
                StringContent content =
                    new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                   await client.PostAsync(request, content);

                if (response.IsSuccessStatusCode)
                {
                    RegisterModel data =
                        await response.Content.ReadAsAsync<RegisterModel>();

                    return data;
                }
                else
                {
                    return null;
                }

            }
        }
        #endregion
    }
}
