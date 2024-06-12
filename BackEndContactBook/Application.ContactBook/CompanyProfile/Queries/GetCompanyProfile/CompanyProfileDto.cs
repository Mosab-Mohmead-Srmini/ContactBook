﻿using ContactBook.Application.Common.Mappings;
using Domain.ContactBook.Entities;

namespace Application.CompanyProfileProfiles.Queries.GetCompanyProfileProfiles
{
    public class CompanyProfileDto : IMapFrom<CompanyProfile>
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;

        public int VATNumber { get; set; } = 0;

        public string Street { get; set; } = string.Empty;

        public string Street2 { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public int Zip { get; set; } = 0;
        public string Country { get; set; } = string.Empty;


    }

}
