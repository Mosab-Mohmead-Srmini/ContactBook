using Application.ContactBook.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ContactBook.Identity
{
    public class CompanyProfileRepository : ICompanyProfileRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public CompanyProfileRepository(IApplicationDbContext dbContext) { 
            
            _dbContext = dbContext;
        }
        public async Task<bool> UniqeCompanyName(string companyName)
        {
            var companyprofileuniqe = await _dbContext.CompanyProfiles.FirstOrDefaultAsync(u => u.CompanyName == companyName);
           if(companyprofileuniqe != null) return true; return false;   
        }
    }
}
