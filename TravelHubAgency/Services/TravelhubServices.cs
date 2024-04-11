using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Numerics;
using TravelHubAgency.Data;
using TravelHubAgency.Helpers;
using TravelHubAgency.Models;

namespace TravelHubAgency.Repositories
{
    public class TravelhubServices
    {
        private string ApiUrl;
        private MediaTypeWithQualityHeaderValue header;

        public TravelhubServices(IConfiguration config)
        {
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
            this.ApiUrl = config.GetValue<string>("ApiUrls:ApiTravelHub");
        }

        // CALLAPISASYNC
        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
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

        #region USUARIOS

        public async Task<Usuario> SigIn(string username, string pass)
        {
            //Usuario user = await this.context.Usuarios.FirstOrDefaultAsync(x => x.Email == username);

            //if (user == null)
            //{
            //    return null;
            //}
            //else
            //{
            //    string salt = user.Salt;
            //    byte[] temp =
            //        HelperCryptography.EncryptPassword(pass, salt);
            //    byte[] passUser = user.Password;

            //    bool response =
            //        HelperTools.CompareArrays(temp, passUser);

            //    if (response == true)
            //    {
            //        return user;
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}

            throw new Exception();
        }

        public async Task<Usuario> SigUp(string nombre, string apellido, string email, string password, string pais)
        {
            //Usuario user = new Usuario();
            //user.IdUsuario = await this.GetMaxIdUsuarioAsync();
            //user.Nombre = nombre;
            //user.Email = email;
            //user.Pais= pais;
            //user.Imagen = "usuario.png";
            //user.TipoUsuario = 2;

            ////CADA USUARIO TENDRA UN SALT DISTINTO 
            //user.Salt = HelperTools.GenerateSalt();

            ////GUARDAMOS EL PASSWORD EN BYTE[] 
            //user.Password =
            //    HelperCryptography.EncryptPassword(password, user.Salt);

            //user.Token = HelperTools.GenerateTokenMail();

            //Usuario existe = await this.context.Usuarios.FirstOrDefaultAsync(x => x.Email == user.Email);

            //if (existe != null)
            //{
            //    return null;
            //}
            //else
            //{
            //    this.context.Usuarios.Add(user);
            //    await this.context.SaveChangesAsync();

            //    return user;
            //}
            throw new Exception();
        }
        #endregion
    }
}
