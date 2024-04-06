using FligthPlanner.Core.Models;

namespace FligthPlanner.Core.Services
{
    public interface IAirportService : IEntityService<Airport>
    {
        List<Airport> SearchAirports(string search);
        void DeleteAirprots();
    }
}
