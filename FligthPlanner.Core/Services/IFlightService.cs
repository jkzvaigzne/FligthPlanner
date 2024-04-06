using FligthPlanner.Core.Models;

namespace FligthPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flights>
    {
        bool DuplicateFlight(Flights flight);
        Flights? FullFlightById(int id);
        Flights CreateFlight(Flights flight);
        List<Airport> SearchAirports(string search);
    }
}
