using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ContactBook.Common.Interfaces
{
    public interface ICompanyProfileRepository
    {
        public Task<bool> UniqeCompanyName(string companyName);
    }
}
