﻿using FlightPlanner.Storage;
using FligthPlanner;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace FlightPlanner.Controllers
{
    [ApiController]
    [Route("testing-api")]
    public class ClearApiController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;

        public ClearApiController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult ClearFlights()
        {
            _context.Airports.RemoveRange(_context.Airports);
            _context.Flights.RemoveRange(_context.Flights);
            _context.SaveChanges();
            
            return Ok();
        }
    }
}