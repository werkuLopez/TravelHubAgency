using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using TravelHubAgency.Data;
using TravelHubAgency.Helpers;
using TravelHubAgency.Models;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                throw ex;
            }
        }

        public async Task<T> CallApiAsyncDelete<T>(string request, string token)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(this.ApiUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.header);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage response = await client.DeleteAsync(request);
                    response.EnsureSuccessStatusCode();

                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
            }
            catch (Exception ex)
            {
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

        public async Task<List<Destino>> GetDestinosPaginadosAsync(int page)
        {
            string request = "api/destinos/alldestinos?page=" + page;
            List<Destino> destinos = await this.CallApiAsync<List<Destino>>(request);
            return destinos;
        }
        public async Task<List<Destino>> GetAllDestinosAsync()
        {
            string request = "api/destinos/alldestinos";
            List<Destino> destinos = await this.CallApiAsync<List<Destino>>(request);
            return destinos;
        }

        public async Task<List<Destino>> GetDestinoByNameAsync(string destino)
        {
            string request = "api/destinos/finddestinosnombre/" + destino;
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

                if (imagen == null || imagen == "")
                {
                    imagen = "default.jpg";
                }

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

        public async Task<Destino> UpdateDestinoAsync(int iddestino, string nombre,
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

                Destino destino = await GetDestinoByIdAsync(iddestino);

                if (imagen == null || imagen == "")
                {
                    imagen = destino.Imagen;
                }

                destino.IdDestino = iddestino;
                destino.Nombre = nombre;
                destino.Region = region;
                destino.IdPais = idpais;
                destino.Descripcion = descripcion;
                destino.Longitud = longitud;
                destino.Latitud = latitud;
                destino.Imagen = imagen;
                destino.Precio = precio;

                string jsonData =
                    JsonConvert.SerializeObject(destino);
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

        public async Task EliminarDestino(int iddestion)
        {
            string token =
                this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;

            using (HttpClient client = new HttpClient())
            {
                string request = "api/destinos/deletedestino/" + iddestion;
                int result = await this.CallApiAsync<int>(request, token);
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

        public async Task<Package> InsertarPackageAsync(Package package)
        {
            string token =
                this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;
            using (HttpClient client = new HttpClient())
            {
                string request = "api/packages/insertar";

                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                Package model = new Package
                {
                    IdPack = 0,
                    Nombre = package.Nombre,
                    FechaInicio = package.FechaInicio,
                    FechaFin = package.FechaFin,
                    Descripcion = package.Descripcion,
                    Personas = package.Personas,
                    Duracion = package.Duracion,
                    Imagen = package.Imagen,
                    Precio = package.Precio,
                    IdDestino = package.IdDestino,
                    IdVuelo = package.IdVuelo,
                    IdHotel = package.IdHotel
                };

                string jsonData =
                    JsonConvert.SerializeObject(model);
                StringContent content =
                    new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                   await client.PostAsync(request, content);

                if (response.IsSuccessStatusCode)
                {
                    Package data =
                        await response.Content.ReadAsAsync<Package>();

                    return data;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<Package> UpdatePackageAsync(Package package)
        {
            string token =
                this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;
            using (HttpClient client = new HttpClient())
            {
                string request = "api/packages/update";

                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                Package model = new Package
                {
                    IdPack = package.IdPack,
                    Nombre = package.Nombre,
                    FechaInicio = package.FechaInicio,
                    FechaFin = package.FechaFin,
                    Descripcion = package.Descripcion,
                    Personas = package.Personas,
                    Duracion = package.Duracion,
                    Imagen = package.Imagen,
                    Precio = package.Precio,
                    IdDestino = package.IdDestino,
                    IdVuelo = package.IdVuelo,
                    IdHotel = package.IdHotel
                };

                string jsonData =
                    JsonConvert.SerializeObject(model);
                StringContent content =
                    new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                   await client.PutAsync(request, content);

                if (response.IsSuccessStatusCode)
                {
                    Package data =
                        await response.Content.ReadAsAsync<Package>();

                    return data;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task EliminarPackage(int idPack)
        {
            string token =
                 this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;

            using (HttpClient client = new HttpClient())
            {
                string request = "api/packages/eliminar/" + idPack;
                int result = await this.CallApiAsync<int>(request, token);
            }

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

            string request = "api/usuarios/perfil";

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
                string request = "api/usuarios/updateperfil";
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

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            List<Usuario> usuarios = await GetAllUsuariosAsync();

            return usuarios.FirstOrDefault(x => x.IdUsuario == id);
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

            string request = "api/usuarios/updatefotoperfil?imagen=" + imagen;

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

        #region VUELOS

        #region calcular distancia/ duración del vuelo/ fecha de llegada
        private static double CalcularDistancia(double lat1, double lon1, double lat2, double lon2)
        {

            double radioTierra = 6371; // radio de la tierra;

            // Convertir las coordenadas de grados a radianes
            double dLat = ConvertToRadians(lat2 - lat1);
            double dLon = ConvertToRadians(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ConvertToRadians(lat1)) * Math.Cos(ConvertToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distancia = radioTierra * c; // Distancia en kilómetros

            return distancia;
        }

        private static double ConvertToRadians(double grados)
        {
            return grados * Math.PI / 180;
        }

        private static DateTime CalcularFechaLlegada(DateTime fechaSalida, double distancia, double velocidad)
        {
            double horas = distancia / velocidad;
            DateTime fechaLlegada = fechaSalida.AddHours(horas);

            return fechaLlegada;
        }
        #endregion

        public async Task<List<Vuelo>> GetAllVuelosAsync()
        {
            string request = "api/vuelos/allvuelos";
            List<Vuelo> vuelos =
                await this.CallApiAsync<List<Vuelo>>(request);

            return vuelos;
        }

        public async Task<Vuelo> GetVueloByIdAsync(int idvuelo)
        {
            string request = "api/vuelos/vuelo/" + idvuelo;
            Vuelo vuelo =
                await this.CallApiAsync<Vuelo>(request);

            return vuelo;
        }

        public async Task<Vuelo> CrearVueloAsync(Vuelo vuelo, string lat2, string lon2)
        {
            string token =
            this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;

            Destino destino = await GetDestinoByIdAsync(vuelo.IdDestino);

            // calculamos la distancia
            double distancia = CalcularDistancia(double.Parse(destino.Latitud),
                double.Parse(destino.Longitud), double.Parse(lat2), double.Parse(lon2));

            using (HttpClient client = new HttpClient())
            {
                string request = "api/vuelos/Insertar";
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                Vuelo newVuelo = new Vuelo();
                newVuelo.IdVuelo = 0;
                newVuelo.Aerolinea = vuelo.Aerolinea;
                newVuelo.IdDestino = vuelo.IdDestino;
                newVuelo.Precio = vuelo.Precio;
                newVuelo.FechaSalida = vuelo.FechaSalida;
                newVuelo.FechaLlegada = CalcularFechaLlegada(vuelo.FechaSalida, distancia, 1200);
                newVuelo.Duracion = "00:00";
                newVuelo.Origen = vuelo.Origen;

                string jsonData =
                    JsonConvert.SerializeObject(newVuelo);
                StringContent content =
                    new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                   await client.PostAsync(request, content);

                if (response.IsSuccessStatusCode)
                {
                    Vuelo comentUloaded =
                        await response.Content.ReadAsAsync<Vuelo>();
                    return comentUloaded;
                }
                else
                {
                    return null;
                }

            }
        }

        public async Task<Vuelo> UpdateVueloAsync(Vuelo vuelo, string lat2, string lon2)
        {
            string token =
            this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;

            Destino destino = await GetDestinoByIdAsync(vuelo.IdDestino);

            // calculamos la distancia
            double distancia = CalcularDistancia(double.Parse(destino.Latitud),
                double.Parse(destino.Longitud), double.Parse(lat2), double.Parse(lon2));

            using (HttpClient client = new HttpClient())
            {
                string request = "api/vuelos/update";
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                Vuelo newVuelo = new Vuelo();
                newVuelo.IdVuelo = vuelo.IdVuelo;
                newVuelo.Aerolinea = vuelo.Aerolinea;
                newVuelo.IdDestino = vuelo.IdDestino;
                newVuelo.Precio = vuelo.Precio;
                newVuelo.FechaSalida = vuelo.FechaSalida;
                newVuelo.FechaLlegada = CalcularFechaLlegada(vuelo.FechaSalida, distancia, 1200);
                newVuelo.Duracion = "00:00:00";
                newVuelo.Origen = vuelo.Origen;

                string jsonData =
                    JsonConvert.SerializeObject(newVuelo);
                StringContent content =
                    new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                   await client.PutAsync(request, content);

                if (response.IsSuccessStatusCode)
                {
                    Vuelo comentUloaded =
                        await response.Content.ReadAsAsync<Vuelo>();
                    return comentUloaded;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<ReservaVuelo> ComprarVueloAsync(int idvuelo, PagoVuelo pago)
        {
            string token =
                this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;
            using (HttpClient client = new HttpClient())
            {
                string request = "api/reservas/vuelos";
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                Vuelo vuelo = await GetVueloByIdAsync(idvuelo);

                PagoVuelo nuevo = new PagoVuelo();
                nuevo.IdVuelo = vuelo.IdVuelo;
                nuevo.MetodoPago = pago.MetodoPago;
                nuevo.FechaPago = DateTime.Now;
                nuevo.Precio = pago.Precio;

                ModelPagoVuelo model = new ModelPagoVuelo
                {
                    Vuelo = vuelo,
                    PagoVuelo = nuevo
                };

                string jsonData =
    JsonConvert.SerializeObject(model);
                StringContent content =
                    new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                   await client.PostAsync(request, content);

                if (response.IsSuccessStatusCode)
                {
                    ReservaVuelo uploaded =
                        await response.Content.ReadAsAsync<ReservaVuelo>();
                    return uploaded;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task EliminarVueloAsync(int idvuelo)
        {
            string token =
                this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;

            using (HttpClient client = new HttpClient())
            {
                string request = "api/Vuelos/EliminarReserva/" + idvuelo;
                int result = await this.CallApiAsync<int>(request, token);
            }
        }

        #endregion

        #region RESERVAS 

        public async Task<ReservaPaquete> ReservarPaqueteAsync(int idPaquete)
        {
            string token =
            this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;

            using (HttpClient client = new HttpClient())
            {
                string request = "api/reservas/paquete";
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                ReservaPaquete paquete = new ReservaPaquete
                {
                    IdPaquete = idPaquete,
                    FechaReserva = DateTime.Now,
                    IdEstadoReserva = 1,
                    IdReserva = 0,
                    IdUsuario = 0 // se recupera del token
                };

                string jsonData =
                        JsonConvert.SerializeObject(paquete);

                StringContent content =
                    new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                   await client.PostAsync(request, content);

                if (response.IsSuccessStatusCode)
                {
                    ReservaPaquete uploaded =
                        await response.Content.ReadAsAsync<ReservaPaquete>();
                    return uploaded;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<ReservaDestino> ReservarDestinoAsync(ReservaDestino reserva)
        {
            string token =
            this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;

            using (HttpClient client = new HttpClient())
            {
                string request = "api/reservas/destino";
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                ReservaDestino destino = new ReservaDestino
                {
                    IdDestino = reserva.IdDestino,
                    FechaReserva = reserva.FechaReserva,
                    IdEstadoReserva = 1,
                    IdReservaDestino = 0,
                    IdUsuario = 0, // se recupera del token
                    FechaReservaFin = reserva.FechaReservaFin,
                    FechaReservaInicio = reserva.FechaReservaInicio,
                    NumPersonas = reserva.NumPersonas,
                    Precio = reserva.Precio
                };

                string jsonData =
                        JsonConvert.SerializeObject(destino);

                StringContent content =
                    new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                   await client.PostAsync(request, content);

                if (response.IsSuccessStatusCode)
                {
                    ReservaDestino uploaded =
                        await response.Content.ReadAsAsync<ReservaDestino>();
                    return uploaded;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<ReservaModel> ReservasUsuarioAsync()
        {
            string token =
            this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;

            string request = "api/reservas/reservasusuario";

            ReservaModel model =
                await this.CallApiAsync<ReservaModel>(request, token);

            return model;
        }

        public async Task EliminarReservaVuelo(int idreservavuelo)
        {
            string token =
    this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;

            using (HttpClient client = new HttpClient())
            {
                string request = "api/vuelos/eliminarreserva/" + idreservavuelo;
                int result = await this.CallApiAsyncDelete<int>(request, token);
            }
        }

        public async Task EliminarReservaDestino(int idreservadestino)
        {
            string token =
    this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;

            using (HttpClient client = new HttpClient())
            {
                string request = "api/reservas/eliminardestino/" + idreservadestino;
                int result = await this.CallApiAsyncDelete<int>(request, token);
            }
        }

        public async Task EliminarReservaPaquete(int idreservapaquete)
        {
            string token =
    this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;

            using (HttpClient client = new HttpClient())
            {
                string request = "api/reservas/eliminarpaquete/" + idreservapaquete;
                int result = await this.CallApiAsyncDelete<int>(request, token);
            }
        }

        public async Task<ReservaHotel> InsertarReservaHotelAsync(ReservaHotel hotel)
        {
            string token =
                this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;

            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Reservas/Hotel";
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                ReservaHotel newHotel = new ReservaHotel
                {
                    IdReservaHotel = 0,
                    Hotel = hotel.Hotel,
                    Fecha = hotel.Fecha,
                    Personas = hotel.Personas,
                    Precio = hotel.Precio,
                    Usuario = 0 //-> token
                };

                string jsonData =
                    JsonConvert.SerializeObject(newHotel);
                StringContent content =
                    new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                   await client.PostAsync(request, content);

                if (response.IsSuccessStatusCode)
                {
                    ReservaHotel h =
                        await response.Content.ReadAsAsync<ReservaHotel>();
                    return h;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task EliminarReservaHotelAsync(int idreservahotel)
        {
            string token =
                this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;

            using (HttpClient client = new HttpClient())
            {
                string request = "api/reservas/eliminarhotel/" + idreservahotel;
                int result = await this.CallApiAsyncDelete<int>(request, token);
            }

        }


        #endregion

        #region POSTSCOMENTARIOSMODEL

        public async Task<PostComentariosModel> GetPostComentariosModelAsync(int idpost)
        {
            PostComentariosModel model = new PostComentariosModel();
            model.Post = await this.GetPostByIdAsync(idpost);
            model.Comentarios = await this.GetComentariosPostAsync(idpost);
            model.Replays = new List<ReplayComentario>();

            foreach (Comentario item in model.Comentarios)
            {
                List<ReplayComentario> replays = await GetReplaysByComentarioAsync(item.IdComentario);

                if (replays != null && replays.Any())
                {
                    foreach (ReplayComentario rp in replays)
                    {
                        model.Replays.Add(rp);
                    }
                }
            }


            return model;
        }

        #endregion

        #region PUBLICACIONES

        public async Task<List<Post>> GetAllPublicacionesAsync(int page)
        {
            string request = "api/publicaciones/allposts?page=" + page;
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
                    FechaPublicacion = DateTime.Now,
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

        public async Task<List<Post>> GetPublicacionesUsuario()
        {
            string token =
                this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;

            string request = "api/publicaciones/postsusuario";

            List<Post> posts =
                await this.CallApiAsync<List<Post>>(request, token);

            return posts;
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

        #region REPLAYSCOMENTARIOS 
        public async Task<List<ReplayComentario>> GetAllReplasy()
        {
            string request = "api/replayscomentario/allreplays";
            List<ReplayComentario> replays =
                await this.CallApiAsync<List<ReplayComentario>>(request);
            return replays;
        }

        public async Task<List<ReplayComentario>> GetReplaysByComentarioAsync(int idcomentario)
        {
            string request = "api/replayscomentario/replayscomentario/" + idcomentario;
            List<ReplayComentario> replays =
                await this.CallApiAsync<List<ReplayComentario>>(request);

            return replays;
        }

        public async Task<ReplayComentario> PublicarReplaysByComentarioAsync(ReplaysModel model)
        {
            string token =
this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;

            using (HttpClient client = new HttpClient())
            {
                string request = "api/replayscomentario/publicarreplay";
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                ReplayComentario newComent = new ReplayComentario
                {
                    IdReplay = 0,
                    Replay = model.Contenido,
                    Fecha = DateTime.Now,
                    IdComentario = model.IdComentario,
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
                    ReplayComentario comentUloaded =
                        await response.Content.ReadAsAsync<ReplayComentario>();
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

        #region HOTELES
        public async Task<List<Hotel>> GetAllHotelesAsync()
        {
            string request = "api/hoteles";

            List<Hotel> hoteles =
                await this.CallApiAsync<List<Hotel>>(request);
            return hoteles;
        }

        public async Task<Hotel> GetHotelByIdAsync(int id)
        {
            string request = "api/hoteles/" + id;
            Hotel hotel =
                await this.CallApiAsync<Hotel>(request);
            return hotel;
        }

        public async Task<Hotel> InsertarHotelAsync(Hotel hotel)
        {
            string token =
            this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;

            using (HttpClient client = new HttpClient())
            {
                string request = "api/hoteles";
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                Hotel newHotel = new Hotel
                {
                    IdHotel = 0,
                    Nombre = hotel.Nombre,
                    Descripcion = hotel.Descripcion,
                    Telefono = hotel.Telefono,
                    Imagen = hotel.Imagen,
                    Precio = hotel.Precio,
                };

                string jsonData =
                    JsonConvert.SerializeObject(newHotel);
                StringContent content =
                    new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                   await client.PostAsync(request, content);

                if (response.IsSuccessStatusCode)
                {
                    Hotel h =
                        await response.Content.ReadAsAsync<Hotel>();
                    return h;
                }
                else
                {
                    return null;
                }

            }
        }

        public async Task<Hotel> UpdateHotelAsync(Hotel hotel)
        {
            string token =
            this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;

            using (HttpClient client = new HttpClient())
            {
                string request = "api/hoteles";
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                Hotel newHotel = new Hotel
                {
                    IdHotel = hotel.IdHotel,
                    Nombre = hotel.Nombre,
                    Descripcion = hotel.Descripcion,
                    Telefono = hotel.Telefono,
                    Imagen = hotel.Imagen,
                    Precio = hotel.Precio,
                };

                string jsonData =
                    JsonConvert.SerializeObject(newHotel);
                StringContent content =
                    new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                   await client.PutAsync(request, content);

                if (response.IsSuccessStatusCode)
                {
                    Hotel h =
                        await response.Content.ReadAsAsync<Hotel>();
                    return h;
                }
                else
                {
                    return null;
                }

            }
        }

        public async Task ELiminarHotelAsync(int id)
        {
            string token =
                this.context.HttpContext.User.FindFirst(x => x.Type == "TOKEN").Value;

            using (HttpClient client = new HttpClient())
            {
                string request = "api/hoteles/" + id;
                int result = await this.CallApiAsyncDelete<int>(request, token);
            }
        }


        #endregion

        #region ESTADO_RESERVA
        public async Task<List<EstadoReserva>> GetEstadoReservaAsync()
        {

            string request = "api/estadosreserva";

            List<EstadoReserva> estados =
                await this.CallApiAsync<List<EstadoReserva>>(request);
            return estados;
        }

        #endregion

        #region ETIQUESTAS

        public async Task<List<Etiqueta>> GetAllEtiquetasAsync()
        {
            string request = "api/etiquetas";

            List<Etiqueta> etiquetas =
                await this.CallApiAsync<List<Etiqueta>>(request);
            return etiquetas;
        }
        #endregion
    }
}
