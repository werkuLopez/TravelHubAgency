using Microsoft.EntityFrameworkCore;
using TravelHubAgency.Data;
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


        #region CONTINENTES
        public async Task<List<Continente>> GetAllContinentesAsync()
        {
            return await this.context.Continentes.ToListAsync();
        }
        #endregion

        #region PAISES

        public async Task<List<Pais>>GetAllPaisesAsync()
        {
            return await this.context.Paises.ToListAsync();
        }

        public async Task<List<Pais>> GetAllPaisesByContinenteAsync(int contiente)
        {
            return await this.context.Paises.Where(x=> x.IdContinente == contiente).ToListAsync();
        }
        #endregion
    }
}
