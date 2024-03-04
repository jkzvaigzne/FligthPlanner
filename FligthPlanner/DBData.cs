using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.EntityFrameworkCore;

namespace FligthPlanner
{
    public class DBData
    {
        private readonly FlightPlannerDbContext _context;

        public DBData(FlightPlannerDbContext context)
        {
            _context = context;
        }

        public Flights? CheckFlightsDuplicateEntry(Flights flight)
        {
            var isSameData = _context.Flights.Where(existingF =>
                existingF.From.AirportCode == flight.From.AirportCode &&
                existingF.From.Country == flight.From.Country &&
                existingF.From.City == flight.From.City &&
                existingF.To.AirportCode == flight.To.AirportCode &&
                existingF.To.Country == flight.To.Country &&
                existingF.To.City == flight.To.City &&
                existingF.Carrier == flight.Carrier &&
                existingF.DepartureTime == flight.DepartureTime &&
                existingF.ArrivalTime == flight.ArrivalTime)
                .ToList().FirstOrDefault();

            return isSameData;
        }

        public bool ValidateFlightsEntry(Flights flight) => new[]
        {
            flight.From.Country, flight.From.City, flight.From.AirportCode,
            flight.To.Country, flight.To.City, flight.To.AirportCode,
            flight.Carrier, flight.DepartureTime, flight.ArrivalTime
        }.All(s => !string.IsNullOrEmpty(s));


        public List<Flights> SearchFlight(SearchFlightsRequest request)
        {
            var flight = _context.Flights
                .Where(f =>
                    f.From.AirportCode.Contains(request.From) &&
                    f.To.AirportCode.Contains(request.To) &&
                    f.DepartureTime.Contains(request.DepartureDate))
                .ToList();

            return flight;
        }

        public List<Airport> SearchAirport(string search)
        {
            string searchModifyLowerTrimmed = search.ToLower().Trim();

            var airPorts = _context.Airports
                .Where(a => a.AirportCode.ToUpper().Contains(searchModifyLowerTrimmed)
                    || a.City.ToUpper().Contains(searchModifyLowerTrimmed)
                    || a.Country.ToUpper().Contains(searchModifyLowerTrimmed))
                .ToList();

            return airPorts;
        }
        public bool ValidateFlightsDate(Flights flight)
        {
            if (!DateTime.TryParse(flight.DepartureTime, out var departureTime) ||
                !DateTime.TryParse(flight.ArrivalTime, out var arrivalTime) ||
                departureTime >= arrivalTime)
            {
                return false;
            }

            return true;
        }
        public Flights SearchFlightById(int id) =>
          _context.Flights
         .Include(f => f.From)
         .Include(f => f.To)
         .FirstOrDefault(s => s.Id == id);
        public bool ValidateFlightDestination(Flights flight) => flight.From.AirportCode.Trim().ToLower() == flight.To.AirportCode.Trim().ToLower();
    }
}
