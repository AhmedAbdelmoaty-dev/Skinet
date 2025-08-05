using Application.Auth.Commands.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommand:IRequest<AuthDto>
    {
       public string RefreshToken { get; set; }
    }
}
