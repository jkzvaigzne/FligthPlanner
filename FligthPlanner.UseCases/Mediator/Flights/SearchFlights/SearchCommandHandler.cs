using FligthPlanner.Core.Models;
using FligthPlanner.Core.Services;
using FligthPlanner.UseCases.Models;
using FluentValidation;
using MediatR;
using System.Net;

namespace FligthPlanner.UseCases.Mediator.Flights.SearchFlights
{
    public class SearchCommandHandler : IRequestHandler<SearchFlightQuery, ServiceResult>
    {
        private readonly ISearchFlightService _searchService;
        private readonly IValidator<SearchFlightsRequest> _validator;

        public SearchCommandHandler(ISearchFlightService searchService, IValidator<SearchFlightsRequest> validator)
            => (_searchService, _validator) = (searchService, validator);

        public async Task<ServiceResult> Handle(SearchFlightQuery request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request.SearchFlightsRequest);

            return validationResult.IsValid
                ? new ServiceResult { ResultObject = _searchService.SearchFlightsRequest(request.SearchFlightsRequest), Status = HttpStatusCode.OK }
                : new ServiceResult { ResultObject = validationResult.Errors, Status = HttpStatusCode.BadRequest };
        }
    }
}
