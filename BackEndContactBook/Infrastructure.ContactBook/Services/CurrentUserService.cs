using ContactBook.Application.Common.Interfaces;

namespace Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string? UserId => throw new NotImplementedException();
    }
}
