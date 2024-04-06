using FligthPlanner.UseCases.Models;
using MediatR;

namespace FligthPlanner.UseCases.Mediator.Flights.AddFlights
{
    public record AddFlightCommand(AddRequestFlight AddRequestFlight) : IRequest<ServiceResult>;
}
