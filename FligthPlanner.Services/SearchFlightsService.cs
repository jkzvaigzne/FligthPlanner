using FligthPlanner.Core.Models;
using FligthPlanner.Core.Services;
using FligthPlanner.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FligthPlanner.Services
{
    public class SearchFlightsService : DbService, ISearchFlightService
    {

        public SearchFlightsService(IFlightPlannerDbContext context) : base(context) { }

        public SearchFlightsResponse SearchFlightsRequest(SearchFlightsRequest request) =>
            new SearchFlightsResponse
            {
                Page = (int)((_context.Flights
                            .Include(f => f.To)
                            .Include(f => f.From)
                            .Count(f => f.DepartureTime.StartsWith(request.DepartureDate)
                                   && f.From.AirportCode == request.From
                                   && f.To.AirportCode == request.To) + 9) / 10),
                TotalItems = _context.Flights
                                .Count(f => f.DepartureTime.StartsWith(request.DepartureDate)
                                      && f.From.AirportCode == request.From
                                      && f.To.AirportCode == request.To),
                Flights = _context.Flights
                            .Include(f => f.To)
                            .Include(f => f.From)
                            .Where(f => f.DepartureTime.StartsWith(request.DepartureDate)
                                      && f.From.AirportCode == request.From
                                      && f.To.AirportCode == request.To)
                            .ToList()
            };
    }
}
