using Application.Auth.Commands.Dtos;
using MediatR;


namespace Application.Auth.Commands.Login
{
    public class LoginCommand:IRequest<AuthDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
