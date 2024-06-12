using ContactBook.Domain.Entities;
using MediatR;
using ContactBook.Application.Common.Exceptions;
using Application.ContactBook.Common.Interfaces;
namespace ContactBook.Application.PassWords.Commands.UpdatePassWord;

public record UpdatePassWordCommand : IRequest
{
    public string Email { get; init; }

    public string Password { get; set; } = string.Empty;

    public string ConfirmPassword { get; set; } = string.Empty;
}

public class UpdatePassWordCommandHandler : IRequestHandler<UpdatePassWordCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdatePassWordCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    async Task<Unit> IRequestHandler<UpdatePassWordCommand, Unit>.Handle(UpdatePassWordCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Users
            .FindAsync(new object[] { request.Email }, cancellationToken);

            if (entity == null )
            {
                throw new NotFoundException(nameof(User), request.Email);
            }

            if (request.Password != request.ConfirmPassword)
            {
                throw new ValidationException("Passwords do not match.");
            }

            if ( request.Password == request.ConfirmPassword) {
        
            entity.Password = request.Password;
            entity.ConfirmPassword = request.ConfirmPassword;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
      


        return Unit.Value;

    }
}
