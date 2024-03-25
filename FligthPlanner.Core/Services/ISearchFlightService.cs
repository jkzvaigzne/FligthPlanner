using FligthPlanner.Core.Models;

namespace FligthPlanner.Core.Services
{
    public interface ISearchFlightService : IDbService
    {
        SearchFlightsResponse SearchFlightsRequest(SearchFlightsRequest request);
    }
}
