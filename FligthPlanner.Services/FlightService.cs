using Microsoft.EntityFrameworkCore;
using FligthPlanner.Core.Models;
using FligthPlanner.Core.Services;
using FligthPlanner.Data;

namespace FligthPlanner.Services
{
    public class FlightService : EntityService<Flights>, IFlightService
    {
        private static readonly object _lock = new();
        private readonly IFlightPlannerDbContext _context;
        public FlightService(IFlightPlannerDbContext context) : base(context) 
        {
            _context = context;
        }

        public Flights CreateFlight(Flights flight)
        {
            lock (_lock)
            {
                if (!_context.Airports.Any(a => a.AirportCode == flight.From.AirportCode))
                    _context.Airports.Add(flight.From);

                if (!_context.Airports.Any(a => a.AirportCode == flight.To.AirportCode))
                    _context.Airports.Add(flight.To);

                return Create(flight);
            }
        }
        public bool DuplicateFlight(Flights flightsRequest)
        {
            return _context.Flights.Any(f =>
                f.From.AirportCode == flightsRequest.From.AirportCode &&
                f.To.AirportCode == flightsRequest.To.AirportCode &&
                f.Carrier == flightsRequest.Carrier &&
                f.DepartureTime == flightsRequest.DepartureTime &&
                f.ArrivalTime == flightsRequest.ArrivalTime);
        }
        public Flights? FullFlightById(int id)
        {
            lock (_lock)
            {
                return _context.Flights
                    .Include(f => f.To)
                    .Include(f => f.From)
                    .SingleOrDefault(f => f.Id == id);
            }
        }
        public List<Airport> SearchAirports(string search)
        {
            return _context.Airports
                .Where(a => a.Country.Contains(search) || a.City.Contains(search) || a.AirportCode.Contains(search))
                .ToList();
        }
    }
}
