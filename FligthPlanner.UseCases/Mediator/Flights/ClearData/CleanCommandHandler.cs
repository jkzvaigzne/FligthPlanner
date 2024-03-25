using FligthPlanner.Core.Services;
using FligthPlanner.UseCases.Models;
using MediatR;

namespace FligthPlanner.UseCases.Mediator.Flights.ClearData
{
    public class CleanCommandHandler : IRequestHandler<CleanCommandQuery, ServiceResult>
    {
        private readonly ICleanupService _cleanService;

        public CleanCommandHandler(ICleanupService cleanService)
        {
            _cleanService = cleanService;
        }

        public Task<ServiceResult> Handle(CleanCommandQuery request, CancellationToken cancellationToken)
        {
            _cleanService.Cleaner();
            return Task.FromResult(new ServiceResult());
        }
    }
}
