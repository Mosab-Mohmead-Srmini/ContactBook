namespace Application.ContactBook.Common.Interfaces
{
    public interface IGenerateToken
    {
        public Task<string> GenerateTokenAsync(int userId);

    }
}
