using Application.ContactBook.Common.Interfaces;
using ContactBook.Application.ContactInformations.Commands.RegisterNewUser;
using FluentValidation;

namespace ContactBook.Application.RegisterNewUsers.Commands.CreateRegisterNewUser;

public class CreateRegisterNewUserCommandValidator : AbstractValidator<RegisterNewUserCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateRegisterNewUserCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.CompanyName)
            .NotEmpty().WithMessage("Company Name is required.")
            .MaximumLength(200).WithMessage("Company Name must not exceed 200 characters.")
            .When(v => !string.IsNullOrEmpty(v.CompanyName));

        RuleFor(v => v.VATNumber)
            .NotEmpty().WithMessage("VAT Number is required.")
            .GreaterThan(0).WithMessage("VAT Number must be a positive integer.")
            .When(v => !string.IsNullOrEmpty(v.CompanyName));

        RuleFor(v => v.Street)
            .NotEmpty().WithMessage("Street is required.")
            .MaximumLength(200).WithMessage("Street must not exceed 200 characters.");

        RuleFor(v => v.Street2)
            .MaximumLength(200).WithMessage("Street2 must not exceed 200 characters.");

        RuleFor(v => v.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(100).WithMessage("City must not exceed 100 characters.");

        RuleFor(v => v.State)
            .MaximumLength(100).WithMessage("State must not exceed 100 characters.");

        RuleFor(v => v.Zip)
            .NotEmpty().WithMessage("Zip is required.")
            .GreaterThan(0).WithMessage("Zip must be a positive integer.");

        RuleFor(v => v.Country)
            .NotEmpty().WithMessage("Country is required.")
            .MaximumLength(100).WithMessage("Country must not exceed 100 characters.");
    }

   
}
