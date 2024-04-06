using FligthPlanner.UseCases.Models;
using MediatR;

namespace FligthPlanner.UseCases.Mediator.Flights.ClearData
{
    public record CleanCommandQuery : IRequest<ServiceResult>;
}
