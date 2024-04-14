using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using TravelHubAgency.Data;
using TravelHubAgency.Helpers;
using TravelHubAgency.Models;

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
            /* using (HttpClient client = new HttpClient())
             {
                 client.BaseAddress = new Uri(this.ApiUrl);
                 client.DefaultRequestHeaders.Clear();
                 client.DefaultRequestHeaders.Accept.Add(this.header);
                 client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                 HttpResponseMessage response =
                     await client.GetAsync(resquest);
                 if (response.IsSuccessStatusCode)
                 {
                     T data = await response.Content.ReadAsAsync<T>();
                     return data;
                 }
                 else
                 {
                     return default(T);
                 }
             }*/

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
            string request = "api/destinos/destinos";
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
            string request = "api/destinos/destinos/" + iddestino;
            Destino destinos = await this.CallApiAsync<Destino>(request);
            return destinos;
        }

        public async Task<Destino> InsertarDestinoAsync(Destino destino)
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
                    Nombre = destino.Nombre,
                    Region = destino.Region,
                    IdPais = destino.IdPais,
                    Descripcion = destino.Descripcion,
                    Longitud = destino.Longitud,
                    Latitud = destino.Latitud,
                    Imagen = destino.Imagen,
                    Precio = destino.Precio
                };

                string jsonData =
                    JsonConvert.SerializeObject(model);
                StringContent content =
                    new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                   await client.PutAsync(request, content);

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
            string request = "api/packages/" + id;
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
