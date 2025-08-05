using Application.Auth.Queries.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Queries.GetUserInfo
{
    public class GetUserInfoQuery:IRequest<UserInfoDto>
    {
        public bool WithAddress { get; set; } = false;
    }
}
