using FligthPlanner.Core.Models;
using FligthPlanner.Validations.Helpers;
using FluentValidation;

namespace FligthPlanner.Validations
{
    public class SearchFlightRequestValidator : AbstractValidator<SearchFlightsRequest>
    {
        public SearchFlightRequestValidator()
        {
            RuleFor(r => r.From)
           .NotEmpty();
            RuleFor(r => r.To)
                .NotEmpty();
            RuleFor(r => r.DepartureDate)
                .NotEmpty()
                .Must(DateValidationHelper.ValidDateTime);
            RuleFor(r => r)
                .Must(r => r.From != r.To);
        }
    }
}
