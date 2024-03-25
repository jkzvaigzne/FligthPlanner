namespace FligthPlanner.Core.Models
{
    public class SearchFlightsResponse
    {
        public List<Flights>? Flights { get; set; }
        public int TotalItems { get; set; }
        public int Page {  get; set; }
    }
}
