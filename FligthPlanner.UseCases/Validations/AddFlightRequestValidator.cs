using FligthPlanner.Core.Models;
using FligthPlanner.UseCases.Validations.Helpers;
using FluentValidation;
using System;

namespace FligthPlanner.UseCases.Validations
{
    public class AddFlightRequestValidator : AbstractValidator<SearchFlightsRequest>
    {
        public AddFlightRequestValidator()
        {
            RuleFor(r => r.From).NotEmpty();
            RuleFor(r => r.To).NotEmpty();
            RuleFor(r => r.DepartureDate).NotEmpty().Must(DateValidationHelper.ValidDateTime);
            RuleFor(r => r).Must(r => r.From != r.To);
            RuleFor(r => r).Must(r => DateValidationHelper.DepartureBeforeArrival(r));
        }
    }
}
