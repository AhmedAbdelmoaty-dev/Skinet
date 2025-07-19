
using Application.Auth.Commands.Dtos;
using MediatR;


namespace Application.Auth.Commands.Register
{
    public class RegisterCommand : IRequest<AuthDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }    
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
