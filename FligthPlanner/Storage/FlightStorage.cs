using FlightPlanner.Models;

namespace FlightPlanner.Storage
{
    public class FlightStorage
    {
        private static List<Flights> _flightStorage = new List<Flights>();

        private static int _ID = 1;
        public void AddFlight(Flights flight)
        {
            flight.Id = _ID++;
            _flightStorage.Add(flight);
        }
        public void RemoveFlight(int id) => 
            _flightStorage.RemoveAll(flight => flight.Id == id);

        public void ClearFlights()
        {
            _flightStorage.Clear();
        }
        public Flights SearchAirport(string search)
        {
            string searchModifyLowerTrimmed = search.ToLower().Trim();

            var flight = _flightStorage
                .FirstOrDefault(s =>
                    s.From.Country.ToLower().Contains(searchModifyLowerTrimmed) ||
                    s.From.City.ToLower().Contains(searchModifyLowerTrimmed) ||
                    s.From.AirportCode.ToLower().Contains(searchModifyLowerTrimmed)
                );

            return flight;
        }

        public List<Flights> SearchFlight(SearchFlightsRequest request)
        {
            var flight = _flightStorage
                .Where(f =>
                    f.From.AirportCode.Contains(request.From) &&
                    f.To.AirportCode.Contains(request.To) &&
                    f.DepartureTime.Contains(request.DepartureDate))
                .ToList();

            return flight;
        }
        public Flights SearchFlightById(int id) => 
            _flightStorage.FirstOrDefault(s => s.Id == id);
        public List<Flights> GetFlights() => _flightStorage;
        public Flights? CheckFlightsDuplicateEntry(Flights flight)
        {
            return _flightStorage.Where(existingF =>
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
        }
        public bool ValidateFlightsEntry(Flights flight) => new[]
        {
            flight.From.Country, flight.From.City, flight.From.AirportCode,
            flight.To.Country, flight.To.City, flight.To.AirportCode,
            flight.Carrier, flight.DepartureTime, flight.ArrivalTime
        }.All(s => !string.IsNullOrEmpty(s));
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

        public bool ValidateFlightDestination(Flights flight) => flight.From.AirportCode.Trim().ToLower() == flight.To.AirportCode.Trim().ToLower();
    }
}