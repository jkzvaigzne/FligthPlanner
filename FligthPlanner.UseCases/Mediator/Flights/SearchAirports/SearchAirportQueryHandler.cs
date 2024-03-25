using AutoMapper;
using FligthPlanner.Core.Services;
using FligthPlanner.UseCases.Models;
using MediatR;
using System.Net;

namespace FligthPlanner.UseCases.Mediator.Flights.SearchAirports
{
    public class SearchAirportQueryHandler : IRequestHandler<SearchAirportQuery, ServiceResult>
    {
        private readonly IAirportService _airService;
        private readonly IMapper _mapper;

        public SearchAirportQueryHandler(IAirportService airService, IMapper mapper)
        {
            _airService = airService;
            _mapper = mapper;
        }
        public async Task<ServiceResult> Handle(SearchAirportQuery request, CancellationToken cancellationToken)
        {
            var airPorts = _airService.SearchAirports(request.search);

            return new ServiceResult()
            {
                ResultObject = _mapper.Map<List<AirportViewModel>>(airPorts),
                Status = HttpStatusCode.OK
            };
        }
    }
}
