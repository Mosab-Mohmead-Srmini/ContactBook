using Application.ContactBook.Common.Interfaces;
using CleanArch.WebUI.Controllers;
using ContactBook.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

public class LoginController : ApiControllerBase
{
    private readonly IGenerateToken _generateToken;
    private readonly IUserRepository _userRepository;

    public LoginController
        (IGenerateToken generateToken, IUserRepository userRepository)
    {
        _generateToken = generateToken;
        _userRepository = userRepository;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<string>>Login([FromBody] LoginRequest request)
    {
        if (request.Email == null)
        {
            return BadRequest("The Email is required"); 
        }
        if (request.Password == null)
        {
            return BadRequest("The Password is required");
        }



        bool isValidCredentials = 
            await _userRepository.ValidateCredentialsAsync(request.Email, request.Password);

        if (!isValidCredentials)
        {
            return BadRequest("Invalid credentials");
        }
      
        var userId = _userRepository.GetUserIdAsync(request.Email).Result;


        string token = await _generateToken.GenerateTokenAsync(userId);

        if (token == null)
        {
            return BadRequest("The Token Is null");
        }
        return Ok(new { Token = token });
    }
}
