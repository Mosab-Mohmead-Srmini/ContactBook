using Application.CompanyProfileProfiles.Queries.GetCompanyProfileProfiles;
using Application.CompanyProfileProfiles.Queries.GetCompanyProfiles;
using Application.CompanyProfilesPro.Queries.GetCompanyProfiles;
using Application.ContactBook.Common.Interfaces;
using CleanArch.WebUI.Controllers;
using ContactBook.Application.CompanyProfiles.Commands.CreateCompanyProfile;
using ContactBook.Application.CompanyProfiles.Commands.UpdateCompanyProfile;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.ContactBook.Controllers
{


    public class CompanyProfilesController : ApiControllerBase
    {
        private  ICompanyProfileRepository _companyprofilerepository;

        public CompanyProfilesController(ICompanyProfileRepository CompanyProfileRepository)
        {
                _companyprofilerepository = CompanyProfileRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CompanyProfileVm>> Get()
        {
            return await Mediator.Send(new CompanyProfileQuery());
        }

        [HttpGet("{ItemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CompanyProfileItemDto>> GetById(int ItemId)

        {
            if (ItemId <= 0) return BadRequest("CompanyProfile ID must be greater than zero...");

            var CompanyProfileVm = await Mediator.Send(new CompanyProfileItemQuery(ItemId));

            if (CompanyProfileVm == null) return NotFound();

            return CompanyProfileVm;
        }


        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateCompanyProfileCommand command)
        {
            bool uniqecompanyname = _companyprofilerepository.UniqeCompanyName(command.CompanyName).Result;
            if (uniqecompanyname) return BadRequest("The specified Company Name already exists...");

            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update(int id, UpdateCompanyProfileCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }



            await Mediator.Send(command);

            return NoContent();
        }
    }
}
