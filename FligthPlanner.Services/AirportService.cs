using FligthPlanner.Services;
using FligthPlanner.Data;
using FligthPlanner.Core.Models;
using FligthPlanner.Core.Services;

namespace FlightPlanner.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        private static readonly object _lock = new();
        private readonly IFlightPlannerDbContext _context;

        public AirportService(IFlightPlannerDbContext context) : base(context) 
        {
            _context = context;
        }

        public List<Airport> SearchAirports(string search)
        {
            search = search.ToUpper().Trim();

            return _context.Airports
                .Where(airport => airport.Country.ToUpper().Contains(search) || airport.City.ToUpper().Contains(search) || airport.AirportCode.ToUpper().Contains(search))
                .ToList();
        }

        public void DeleteAirprots()
        {
            lock (_lock)
            {
                var airports = _context.Airports
                    .Where(a => !_context.Flights.Any(f => f.From.Id == a.Id || f.To.Id == a.Id))
                    .ToList();

                _context.Airports.RemoveRange(airports);
                _context.SaveChanges();
            }
        }
    }
}
