using Microsoft.AspNetCore.Mvc;
using Afonin.AuthService.Domain.Models;
using Afonin.AuthService.Domain.Exceptions;
using Afonin.AuthService.Domain.Interfaces;

namespace AdAstra.HRPlatform.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(response);
        }

        [HttpPost("refresh")]
        public IActionResult Refresh(TokensRequest model)
        {
            AuthenticateResponse? response;
            try
            {
                response = _userService.UpdateToken(model);

                if (response == null)
                {
                    return BadRequest(new { message = "Username or password is incorrect" });
                }
            } catch (ServiceLayerException ex)
            {
                // TODO: add logger
                return BadRequest(ex);
            }

            return Ok(response);
        }
    }
}
