using Microsoft.EntityFrameworkCore;
using FligthPlanner.Core.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FligthPlanner.Data;

public interface IFlightPlannerDbContext
{
    EntityEntry<T> Entry<T>(T entity) where T : class;
    DbSet<T> Set<T>() where T : class;
    DbSet<Airport> Airports { get; set; }
    DbSet<Flights> Flights { get; set; }
    int SaveChanges();
}