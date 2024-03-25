using FligthPlanner.Core.Services;
using FligthPlanner.UseCases.Models;
using MediatR;

namespace FligthPlanner.UseCases.Mediator.Flights.DeleteFlights
{
    public class DeleteCommandHandler : IRequestHandler<DeleteFlightCommand, ServiceResult>
    {
        private readonly IFlightService _flightService;
        private readonly IAirportService _airportService;

        public DeleteCommandHandler(IFlightService flightService, IAirportService airportService)
            => (_flightService, _airportService) = (flightService, airportService);

        public async Task<ServiceResult> Handle(DeleteFlightCommand request, CancellationToken cancellationToken)
        {
            var flight = _flightService.GetById(request.id);
            if (flight != null)
            {
                _flightService.Delete(flight);
                _airportService.DeleteAirprots();
            }

            return new();
        }
    }
}
