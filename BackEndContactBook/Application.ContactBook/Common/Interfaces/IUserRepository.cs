using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ContactBook.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ValidateCredentialsAsync(string email, string password);
        Task<bool> VerifyPasswordAsync(string password);
        Task<int> GetUserIdAsync(string email);
    }
}
