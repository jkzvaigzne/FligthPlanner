using AutoMapper;
using FligthPlanner.Core.Models;
using FligthPlanner.Core.Services;
using FligthPlanner.UseCases.Models;
using FluentValidation;
using MediatR;
using System.Net;

namespace FligthPlanner.UseCases.Mediator.Flights.AddFlights
{
    public class AddCommandHandler : IRequestHandler<AddFlightCommand, ServiceResult>
    {
        private readonly IValidator<AddRequestFlight> _validator;
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;

        public AddCommandHandler(
            IValidator<AddRequestFlight> validator,
            IFlightService flightService,
            IMapper mapper)
        {
            _validator = validator;
            _flightService = flightService;
            _mapper = mapper;
        }

        public async Task<ServiceResult> Handle(AddFlightCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request.AddRequestFlight);

            if (!validationResult.IsValid)
                return new ServiceResult();

            var flight = _mapper.Map<FligthPlanner.Core.Models.Flights>(request.AddRequestFlight); 

            if (_flightService.DuplicateFlight(flight))
                return new ServiceResult { ResultObject = _mapper.Map<SearchFlightsRequest>(flight), Status = HttpStatusCode.Conflict };

            _flightService.CreateFlight(flight);

            return new ServiceResult { ResultObject = _mapper.Map<SearchFlightsRequest>(flight), Status = HttpStatusCode.Created };
        }
    }
}
