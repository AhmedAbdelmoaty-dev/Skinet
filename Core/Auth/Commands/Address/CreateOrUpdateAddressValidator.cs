using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Commands.Address
{
    public class CreateOrUpdateAddressValidator:AbstractValidator<CreateOrUpdateAddressCommand>
    {
        public CreateOrUpdateAddressValidator()
        {
            RuleFor(x => x.Line1)
                .NotEmpty().WithMessage("Line1 is required.")
                .MaximumLength(100).WithMessage("Line1 must not exceed 100 characters.");

            RuleFor(x => x.City).NotEmpty().WithMessage("City is required.")
                .MaximumLength(50).WithMessage("City must not exceed 50 characters.");
            
            RuleFor(x => x.State).NotEmpty().WithMessage("State is required.");

            RuleFor(x => x.PostalCode)
                .NotEmpty().WithMessage("PostalCode is required.")
                .Matches(@"^\d{5}(-\d{4})?$").WithMessage("PostalCode must be in the format 12345 or 12345-6789.");

            RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required.")
                .MaximumLength(50).WithMessage("Country must not exceed 50 characters.");

        }
    }
}
