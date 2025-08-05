
using MediatR;

namespace Application.Auth.Commands.Logout
{
    public class LogoutCommand:IRequest
    {
       public string RefreshToken { get; set; }   
    }
}
