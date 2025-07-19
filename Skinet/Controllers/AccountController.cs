using Application.Auth.Commands.Login;
using Application.Auth.Commands.Logout;
using Application.Auth.Commands.Register;
using Application.Auth.Queries.GetUserInfo;
using Infrastructure.IdentityEntities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Skinet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<AppUser> _userManager;
        public AccountController(IMediator mediator, UserManager<AppUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterCommand command)
        {

            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginCommand command)
        {
            var result = await _mediator.Send(command);
            SetRefreshToken(result.RefreshToken,result.RefreshTokenExpiration);
            return Ok(result);
        }

        
        [HttpGet("userinfo")]
    
        public async Task<ActionResult> GetUserInfo()
        {

            var result = await _mediator.Send(new GetUserInfoQuery { user = User });
            return Ok(result);

        }
        [HttpPost("logout")]
        public async Task<ActionResult> Logout(LogoutCommand command)
        {
            
            Response.Cookies.Delete("refreshtoken");
            await _mediator.Send(command);
            return Ok();
        }



        private void SetRefreshToken(string refreshToken,DateTime expires )
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime()
            };
            Response.Cookies.Append("refreshtoken", refreshToken, cookieOptions);
        }
    }
}
