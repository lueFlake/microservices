using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Afonin.AuthService.Domain.Entities;
using Afonin.AuthService.Domain.Models;
using Afonin.AuthService.Domain.Exceptions;
using Afonin.AuthService.Domain.Interfaces;

namespace AdAstra.HRPlatform.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthenticateResponse), 200)]
        public async Task<IActionResult> Register(RegisterRequest userModel)
        {
            try
            {
                var response = await _userService.Register(userModel);

                if (response == null)
                {
                    return BadRequest(new { message = "Didn't register!" });
                }

                return Ok(response);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Validation Error: {ex.Message}");
            }
            catch (ServiceLayerException ex)
            {
                return BadRequest($"Service Error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("me")]
        [ProducesResponseType(typeof(RegisterRequest), 200)]
        public IActionResult GetMe()
        {
            var user = _userService.GetAll()
                .FirstOrDefault(u => 
                u.Username == User.Identity?.Name);
            if (user == null)
            {
                return NotFound("User is not found");
            }
            return Ok(user);
        }
    }
}
