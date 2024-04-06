using FlightPlanner.Handlers;
using FlightPlanner.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FligthPlanner.Data;
using FligthPlanner.Core.Services;
using FligthPlanner.Services;
using FligthPlanner.Core.Models;

namespace FlightPlanner
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Connection
            builder.Services.AddDbContext<FlightPlannerDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("flight-planner")));
            // Customs
            builder.Services.AddTransient<IFlightPlannerDbContext, FlightPlannerDbContext>();
            builder.Services.AddTransient<IDbService, DbService>();
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            builder.Services.AddTransient<IAirportService, AirportService>();
            builder.Services.AddTransient<ICleanupService, CleanupService>();
            builder.Services.AddTransient<IFlightService, FlightService>();
            builder.Services.AddTransient<IEntityService<Airport>, EntityService<Airport>>();
            builder.Services.AddTransient<IEntityService<Flights>, EntityService<Flights>>();
            // Register
            builder.Services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}