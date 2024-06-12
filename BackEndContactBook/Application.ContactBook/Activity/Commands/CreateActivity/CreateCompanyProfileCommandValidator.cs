using Application.ContactBook.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.Application.Activitys.Commands.CreateActivity;

public class CreateActivityCommandValidator : AbstractValidator<CreateActivityCommand>
{
    public CreateActivityCommandValidator(IApplicationDbContext context)
    {
    

        RuleFor(v => v.Action)
                .NotEmpty().WithMessage("Action is required.");

        RuleFor(v => v.ByUserId)
            .NotEmpty().WithMessage("User ID is required.")
            .GreaterThan(0).WithMessage("User ID must be greater than 0.");

        RuleFor(v => v.Date)
            .NotEmpty().WithMessage("Date is required.");

        RuleFor(v => v.ContactId)
            .NotEmpty().WithMessage("Contact ID is required.")
            .GreaterThan(0).WithMessage("Contact ID must be greater than 0.");


    }

}
