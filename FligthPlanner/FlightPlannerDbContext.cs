﻿using FlightPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace FligthPlanner
{
    public class FlightPlannerDbContext: DbContext
    {
        public FlightPlannerDbContext(DbContextOptions<FlightPlannerDbContext>
            options) : base(options)
        {
            
        }
        public DbSet<Flights> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
    }
}
