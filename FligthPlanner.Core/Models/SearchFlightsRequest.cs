namespace FligthPlanner.Core.Models
{
    public class SearchFlightsRequest
    {
        public required string DepartureDate { get; set; }
        public required string From { get; set; }
        public required string To { get; set; }
        public required string Arrival {  get; set; }
    }
}