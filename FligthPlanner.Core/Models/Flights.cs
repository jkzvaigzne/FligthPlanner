

namespace FligthPlanner.Core.Models
{
    public class Flights : Entity
    {
        public required string DepartureTime { get; set; }
        public required string ArrivalTime { get; set; }
        public required string Carrier { get; set; }
        public required Airport From { get; set; }
        public required Airport To { get; set; }
    }
}