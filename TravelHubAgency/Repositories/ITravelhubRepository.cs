using TravelHubAgency.Models;

namespace TravelHubAgency.Repositories
{
    public interface ITravelhubRepository
    {
        #region CONTINENTES
        Task<List<Continente>> GetAllContinentesAsync();
        #endregion

        #region PAISES
        Task<List<Pais>> GetAllPaisesAsync();
        Task<List<Pais>> GetAllPaisesByContinenteAsync(int contiente);
        #endregion

        #region DESTINOS
        //Task<List<Destino>> GetAllDestinosAsync();
        //Task<Destino> GetDestinoByNameAsync(string destino);
        //Task<Destino> GetDestinoByIdAsync(int iddestino);
        #endregion

        #region RESERVAS

        #endregion

        #region USUARIOS
        #endregion
    }
}
