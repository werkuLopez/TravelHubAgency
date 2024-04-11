using Microsoft.EntityFrameworkCore;
using TravelHubAgency.Data;
using TravelHubAgency.Helpers;
using TravelHubAgency.Models;

namespace TravelHubAgency.Repositories
{
    public class TravelhubRepository : ITravelhubRepository
    {
        private TravelhubContext context;

        public TravelhubRepository(TravelhubContext context)
        {
            this.context = context;
        }

        // MAXID
        private async Task<int> GetMaxIdUsuarioAsync()
        {
            if (this.context.Usuarios.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await
                    this.context.Usuarios.MaxAsync(z => z.IdUsuario) + 1;
            }
        }
        private async Task<int> GetMaxIdContinentesAsync()
        {
            if (this.context.Continentes.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await
                    this.context.Continentes.MaxAsync(z => z.IdContinente) + 1;
            }
        }
        private async Task<int> GetMaxIdPaisesAsync()
        {
            if (this.context.Paises.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await
                    this.context.Paises.MaxAsync(z => z.IdPais) + 1;
            }
        }

        #region CONTINENTES

        public async Task<List<Continente>> GetAllContinentesAsync()
        {
            return await this.context.Continentes.ToListAsync();
        }
        #endregion

        #region PAISES

        public async Task<List<Pais>> GetAllPaisesAsync()
        {
            return await this.context.Paises.ToListAsync();
        }

        public async Task<List<Pais>> GetAllPaisesByContinenteAsync(int contiente)
        {
            return await this.context.Paises.Where(x => x.IdContinente == contiente).ToListAsync();
        }

        #endregion

        #region USUARIOS

        public async Task<Usuario> SigIn(string username, string pass)
        {
            Usuario user = await this.context.Usuarios.FirstOrDefaultAsync(x => x.Email == username);

            if (user == null)
            {
                return null;
            }
            else
            {
                string salt = user.Salt;
                byte[] temp =
                    HelperCryptography.EncryptPassword(pass, salt);
                byte[] passUser = user.Password;

                bool response =
                    HelperTools.CompareArrays(temp, passUser);

                if (response == true)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<Usuario> SigUp(string nombre, string apellido, string email, string password, string pais)
        {
            Usuario user = new Usuario();
            user.IdUsuario = await this.GetMaxIdUsuarioAsync();
            user.Nombre = nombre;
            user.Email = email;
            user.Pais= pais;
            user.Imagen = "usuario.png";
            user.TipoUsuario = 2;

            //CADA USUARIO TENDRA UN SALT DISTINTO 
            user.Salt = HelperTools.GenerateSalt();

            //GUARDAMOS EL PASSWORD EN BYTE[] 
            user.Password =
                HelperCryptography.EncryptPassword(password, user.Salt);

            user.Token = HelperTools.GenerateTokenMail();

            Usuario existe = await this.context.Usuarios.FirstOrDefaultAsync(x => x.Email == user.Email);

            if (existe != null)
            {
                return null;
            }
            else
            {
                this.context.Usuarios.Add(user);
                await this.context.SaveChangesAsync();

                return user;
            }
        }
        #endregion
    }
}
