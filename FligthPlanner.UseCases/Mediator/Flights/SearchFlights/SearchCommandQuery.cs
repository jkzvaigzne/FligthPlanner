using FligthPlanner.Core.Models;
using FligthPlanner.UseCases.Models;
using MediatR;

namespace FligthPlanner.UseCases.Mediator.Flights.SearchFlights
{
    public record SearchFlightQuery(SearchFlightsRequest SearchFlightsRequest) : IRequest<ServiceResult>;
}