using FligthPlanner.Core.Models;
using FligthPlanner.Core.Services;
using FligthPlanner.Data;

namespace FligthPlanner.Services
{
    public class CleanupService : DbService, ICleanupService
    {
        public CleanupService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public void Cleaner()
        {
            DeleteAll<Flights>();
            DeleteAll<Airport>();
        }
    }
}
