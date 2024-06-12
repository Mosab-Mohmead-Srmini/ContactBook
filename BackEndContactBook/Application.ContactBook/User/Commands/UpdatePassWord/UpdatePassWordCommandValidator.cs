using Application.ContactBook.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.Application.PassWords.Commands.UpdatePassWord;

public class UpdatePassWordCommandValidator : AbstractValidator<UpdatePassWordCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdatePassWordCommandValidator(IApplicationDbContext context)
    {
        _context = context;


        RuleFor(v => v.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(200).WithMessage("Email must not exceed 200 characters.")
            .EmailAddress().WithMessage("Email must be a valid email address.")
            .MustAsync(BeUniqueEmail).WithMessage("The specified email already exists.");
    }

    private async Task<bool> BeUniqueEmail(UpdatePassWordCommand model, string email, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Where(u => u.Email != model.Email)
            .AllAsync(u => u.Email != email, cancellationToken);
    }
}
