using FligthPlanner.UseCases.Models;
using MediatR;

namespace FligthPlanner.UseCases.Mediator.Flights.GetFlights
{
    public record GetFlightsQuery(int id) : IRequest<ServiceResult>;

}
