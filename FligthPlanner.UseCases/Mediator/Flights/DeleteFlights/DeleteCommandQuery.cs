using FligthPlanner.UseCases.Models;
using MediatR;

namespace FligthPlanner.UseCases.Mediator.Flights.DeleteFlights
{
    public record DeleteFlightCommand(int id) : IRequest<ServiceResult>;

}
