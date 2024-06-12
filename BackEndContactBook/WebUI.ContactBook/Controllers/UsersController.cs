using Application.ContactBook.Common.Interfaces;
using Application.UserProfiles.Queries.GetUserProfiles;
using Application.UserProfiles.Queries.GetUsers;
using Application.UsersPro.Queries.GetUsers;
using CleanArch.WebUI.Controllers;
using ContactBook.Application.ContactInformations.Commands.RegisterNewUser;
using ContactBook.Application.Users.Commands.DeleteUser;
using ContactBook.Application.Users.Commands.InviteNewUser;
using ContactBook.Application.Users.Commands.UpdateUser;
using Domain.ContactBook.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace WebUI.ContactBook.Controllers
{


    public class UsersController : ApiControllerBase
    {
        private IUserRepositorySignIn _userRepository { get; }

        private  ILogger<UsersController> _logger {  get; }



        public UsersController(
            IUserRepositorySignIn userRepository,
            ILogger<UsersController> logger
            )
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<UserVm>> Get(int pageNumber = 1, int pageSize = 6)
        {
            var query = new UserQuery(pageNumber, pageSize);
            return await Mediator.Send(query);
        }

        [HttpGet("{ItemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserItemDto>> GetById(int ItemId)

        {
            if (ItemId <= 0) return BadRequest("User ID must be greater than zero...");

            var UserVm = await Mediator.Send(new UserItemQuery(ItemId));

            if (UserVm == null) return NotFound();

            return UserVm;
        }


        [HttpPost("InviteNewUser")]
        [Authorize]
        public async Task<ActionResult<int>> InviteNewUser(InviteNewUserCommand command)
        {
            // Extract the logged-in user's ID from the claims
            var userId =User.FindFirstValue(ClaimTypes.NameIdentifier);

           
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID is missing from the token.");
            }

            // Get the inviting user by ID
            var invitingUser = await _userRepository.GetUserByIdAsync(userId);

            if (invitingUser == null || invitingUser.UserType != UserRole.Admin)
            {
                return BadRequest("You are not allowed to invite users.");
            }

            // Check if the email is unique
            bool uniqueEmail = await _userRepository.UniqeEmail(command.Email);
            if (!uniqueEmail)
            {
                return BadRequest("The specified email already exists.");
            }

            // Add the inviting user's ID to the command
           // command.InvitedBy = invitingUser.Email;

            // Send the command to the handler
            int newUserId = await Mediator.Send(command);
            return Ok(newUserId);
        }



        [HttpPost("RegisterNewUser")]
        public async Task<ActionResult<int>> RegisterNewUser(RegisterNewUserCommand command)
        {
            bool uniqueEmail = await _userRepository.UniqeEmail(command.Email);
            if (uniqueEmail)
            {
                return BadRequest("The specified email already exists.");
            }

                int userId = await Mediator.Send(command);
                return Ok(userId);
       
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update(int id, UpdateUserCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            await Mediator.Send(command);

            return NoContent();
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteUserCommand(id));

            return NoContent();
        }
    }
}
