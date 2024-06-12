using Application.ContactBook.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.Application.Contacts.Commands.UpdateContact;

public class UpdateContactCommandValidator : AbstractValidator<UpdateContactCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateContactCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.FirstName)
               .NotEmpty().WithMessage("First Name is required.")
               .MaximumLength(100).WithMessage("First Name must not exceed 100 characters.");

        RuleFor(v => v.LastName)
            .NotEmpty().WithMessage("Last Name is required.")
            .MaximumLength(100).WithMessage("Last Name must not exceed 100 characters.");

        RuleFor(v => v.Email)
           .NotEmpty().WithMessage("Email is required.")
           .MaximumLength(100).WithMessage("Email must not exceed 100 characters.")
           .MustAsync(BeUniqueEmail).WithMessage("The specified title already exists.");

        RuleFor(v => v.Phone)
            .MaximumLength(15).WithMessage("Phone number must not exceed 15 characters.");

        RuleFor(v => v.Email2)
            .EmailAddress().WithMessage("A valid secondary email is required.")
            .When(v => !string.IsNullOrEmpty(v.Email2));

        RuleFor(v => v.Mobile)
            .MaximumLength(15).WithMessage("Mobile number must not exceed 15 characters.");

        RuleFor(v => v.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(200).WithMessage("Address must not exceed 200 characters.");

        RuleFor(v => v.Address2)
            .MaximumLength(200).WithMessage("Address2 must not exceed 200 characters.");

        RuleFor(v => v.Image)
            .MaximumLength(200).WithMessage("Image path must not exceed 200 characters.");

    }
    public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return await _context.Contacts
            .AllAsync(l => l.Email != email, cancellationToken);
    }
}
