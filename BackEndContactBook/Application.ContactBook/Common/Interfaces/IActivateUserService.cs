namespace Application.ContactBook.Common.Interfaces
{
    public interface IActivateUserService
    {
        Task ActivateUserAsync(string email);
    }
}
