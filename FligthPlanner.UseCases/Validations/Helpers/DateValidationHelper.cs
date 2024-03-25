
using FligthPlanner.Core.Models;

namespace FligthPlanner.UseCases.Validations.Helpers
{
    public static class DateValidationHelper
    {
        public static bool ValidDateTime(string date) => DateTime.TryParse(date, out _);

        public static bool DepartureBeforeArrival(SearchFlightsRequest searchFlightsRequest)
            => DateTime.Parse(searchFlightsRequest.DepartureDate) < DateTime.Parse(searchFlightsRequest.Arrival);
    }

}
