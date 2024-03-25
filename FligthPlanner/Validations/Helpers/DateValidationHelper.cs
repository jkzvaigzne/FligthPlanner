
using FligthPlanner.Core.Models;
using FligthPlanner.Models;

namespace FligthPlanner.Validations.Helpers
{
    public static class DateValidationHelper
    {
        public static bool ValidDateTime(string date) => DateTime.TryParse(date, out _);

        public static bool DepartureBeforeArrival(SearchFlightsRequest searchFlightsRequest)
            => DateTime.Parse(searchFlightsRequest.DepartureDate) < DateTime.Parse(searchFlightsRequest.Arrival);
    }

}
