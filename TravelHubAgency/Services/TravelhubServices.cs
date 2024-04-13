using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Numerics;
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
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
            this.ApiUrl = config.GetValue<string>("ApiUrls:ApiTravelHub");
            this.context = context;
        }

        #region CALLAPISASYNC
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
            string request = "api/travelhub/continentes";
            List<Continente> data = await this.CallApiAsync<List<Continente>>(request);
            return data;
        }
        #endregion

        #region PAISES

        public async Task<List<Pais>> GetAllPaisesAsync()
        {
            string request = "api/travelhub/continentes";
            List<Pais> data = await this.CallApiAsync<List<Pais>>(request);
            return data;
        }

        public async Task<List<Pais>> GetAllPaisesByContinenteAsync(int contiente)
        {
            string request = "api/travelhub/paisescontinente/" + contiente;
            List<Pais> data = await this.CallApiAsync<List<Pais>>(request);
            return data;
        }

        #endregion

        #region DESTINOS 
        public async Task<List<Destino>> GetAllDestinosAsync()
        {
            string request = "api/travelhub/destinos";
            List<Destino> destinos = await this.CallApiAsync<List<Destino>>(request);
            return destinos;
        }

        public async Task<Destino> GetDestinoByNameAsync(string destino)
        {
            string request = "api/travelhub/destinos?destino=" + destino;
            Destino destinos = await this.CallApiAsync<Destino>(request);
            return destinos;
        }

        public async Task<Destino> GetDestinoByIdAsync(int iddestino)
        {
            string request = "api/travelhub/destinos/" + iddestino;
            Destino destinos = await this.CallApiAsync<Destino>(request);
            return destinos;
        }
        #endregion

        #region USUARIOS

        public async Task<List<Usuario>> GetAllUsuariosAsync()
        {
            string request = "api/travelhub/usuarios";
            List<Usuario> usuarios =
                await this.CallApiAsync<List<Usuario>>(request);

            return usuarios;
        }
        #endregion

        #region AUTH

        public async Task<RegisterModel> SigUp(string nombre, string apellido, string email, string password, string pais)
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
