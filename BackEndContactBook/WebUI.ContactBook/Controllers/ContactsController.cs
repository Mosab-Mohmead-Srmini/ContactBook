using Application.ContactProfiles.Queries.GetContactProfiles;
using Application.ContactProfiles.Queries.GetContacts;
using Application.ContactsPro.Queries.GetContacts;
using CleanArch.WebUI.Controllers;
using ContactBook.Application.Contacts.Commands.CreateContact;
using ContactBook.Application.Contacts.Commands.UpdateContact;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.ContactBook.Controllers
{


    public class ContactsController : ApiControllerBase
    {
       

        [HttpGet]
        public async Task<ActionResult<ContactVm>> Get()
        {
            return await Mediator.Send(new ContactQuery());
        }

        [HttpGet("{ItemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ContactItemDto>> GetById(int ItemId)

        {
            if (ItemId <= 0) return BadRequest("Contact ID must be greater than zero...");

            var ContactVm = await Mediator.Send(new ContactItemQuery(ItemId));

            if (ContactVm == null) return NotFound();

            return ContactVm;
        }


        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateContactCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update(int id, UpdateContactCommand command)
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
