using Microsoft.AspNetCore.Mvc;
using ResearchesUFU.API.Models.DTO.Requests;
using ResearchesUFU.API.Models.DTO.Responses;
using ResearchesUFU.API.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ResearchesUFU.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly IUserService _userService;

        public SessionController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// performs the login process
        /// </summary>
        /// <returns>The <see cref="UserAuthenticationResponseDto">info about the authenticate user</see></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserAuthenticationResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody, Required] UserAuthenticationRequestDto userAuthenticationRequest)
        {
            var response = await _userService.AuthenticateUserAsync(userAuthenticationRequest);

            return response.HttpStatusCode switch
            {
                StatusCodes.Status200OK => Ok(response.Content),
                StatusCodes.Status404NotFound => NotFound(),
                StatusCodes.Status400BadRequest => BadRequest(response.Content),
                _ => StatusCode(response.HttpStatusCode)
            };
        }
    }
}