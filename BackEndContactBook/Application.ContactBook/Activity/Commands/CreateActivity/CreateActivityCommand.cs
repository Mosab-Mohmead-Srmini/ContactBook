using Application.ContactBook.Common.Interfaces;
using Domain.ContactBook.Entities;
using MediatR;

namespace ContactBook.Application.Activitys.Commands.CreateActivity;

public record CreateActivityCommand : IRequest<int>
{
    public string Action { get; set; }
    public int ByUserId { get; set; }
    public DateTime Date { get; set; }
    public int ContactId { get; set; }


}

public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateActivityCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        var entity = new Activity();

        entity.Action = request.Action;
        entity.ByUserId = request.ByUserId;
        entity.Date = request.Date;
        entity.ContactId = request.ContactId;


        _context.Activitys.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
        
        return entity.Id;
    }
}
