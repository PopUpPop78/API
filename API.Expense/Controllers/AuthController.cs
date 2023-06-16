using Data.Contracts;
using Data.Users;
using Data.ValidationFilters;
using Microsoft.AspNetCore.Mvc;

namespace API.Expense.Controllers
{
    [ServiceFilter(type: typeof(ValidationFilterAttribute))]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        // POST: api/Auth/login
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginUser user)
        {
            var authUser = await _authManager.Login(user);
            if (authUser == null)
            {
                return Unauthorized();
            }

            return Ok(authUser);
        }

        // POST: api/Auth/login
        [HttpPost]
        [Route("refreshtoken")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RefreshToken(AuthUser user)
        {
            var authUser = await _authManager.VerifyRefreshToken(user);
            if (authUser == null)
            {
                return BadRequest();
            }

            return Ok(authUser);
        }

        // POST: api/Auth/register
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(CreateUser user)
        {
            var errors = await _authManager.Register(user);

            if(errors.Any())
            {
                foreach(var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                    return BadRequest(ModelState);
                }
            }

            return Ok();
        }
    }
}
