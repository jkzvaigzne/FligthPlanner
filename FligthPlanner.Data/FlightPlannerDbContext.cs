﻿using FligthPlanner.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FligthPlanner.Data;

public class FlightPlannerDbContext : DbContext, IFlightPlannerDbContext
{
    public FlightPlannerDbContext(DbContextOptions<FlightPlannerDbContext> options) : base(options)
    {

    }

    public DbSet<Airport> Airports { get; set; }
    public DbSet<Flights> Flights { get; set; }
}