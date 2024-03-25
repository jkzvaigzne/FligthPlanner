using FligthPlanner.UseCases.Models;
using MediatR;

namespace FligthPlanner.UseCases.Mediator.Flights.SearchAirports
{
    public record SearchAirportQuery(string search) : IRequest<ServiceResult>;
}
