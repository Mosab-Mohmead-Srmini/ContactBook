using Application.ContactBook.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateUserCommandValidator(IApplicationDbContext context)
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
            .MaximumLength(200).WithMessage("Email must not exceed 200 characters.")
            .EmailAddress().WithMessage("Email must be a valid email address.")
            .MustAsync(BeUniqueEmail).WithMessage("The specified email already exists.");

        RuleFor(v => v.Phone)
            .MaximumLength(15).WithMessage("Phone must not exceed 15 characters.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone must be a valid phone number.");

    }

    private async Task<bool> BeUniqueEmail(UpdateUserCommand model, string email, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Where(u => u.Id != model.Id)
            .AllAsync(u => u.Email != email, cancellationToken);
    }
}
