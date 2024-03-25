using AutoMapper;
using FligthPlanner.Core.Services;
using FligthPlanner.UseCases.Models;
using MediatR;
using System.Net;

namespace FligthPlanner.UseCases.Mediator.Flights.GetFlights
{
    public class GetCommandHandler : IRequestHandler<GetFlightsQuery, ServiceResult>
    {
        private readonly IMapper _mapper;
        private readonly IFlightService _flightService;

        public GetCommandHandler(IMapper mapper, IFlightService flightService)
            => (_mapper, _flightService) = (mapper, flightService);

        public async Task<ServiceResult> Handle(GetFlightsQuery request, CancellationToken cancellationToken)
        {
            var flight = _flightService.GetById(request.id);

            return flight == null
                ? new ServiceResult { Status = HttpStatusCode.NotFound }
                : new ServiceResult { ResultObject = _mapper.Map<AddResponseFlight>(flight), Status = HttpStatusCode.OK };
        }
    }
}
