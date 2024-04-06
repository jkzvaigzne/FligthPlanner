using FligthPlanner.UseCases.Models;
using FluentValidation;

namespace FligthPlanner.UseCases.Validations
{
    public class AirportViewModelValidator : AbstractValidator<AirportViewModel>
    {
        public AirportViewModelValidator()
        {
            RuleFor(airport => airport.Airport)
            .NotEmpty();
            RuleFor(airport => airport.City)
                .NotEmpty();
            RuleFor(airport => airport.Country)
                .NotEmpty();
        }
    }
}
