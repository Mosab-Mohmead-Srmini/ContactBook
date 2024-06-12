
using Application.CompanyProfileProfiles.Queries.GetCompanyProfileProfiles;

namespace Application.CompanyProfileProfiles.Queries.GetCompanyProfiles
{
    public class CompanyProfileVm
    {
        public IReadOnlyCollection<CompanyProfileDto> Lists { get; init; } = Array.Empty<CompanyProfileDto>();

    }
}



