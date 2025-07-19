using AutoMapper;
using Infrastructure.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Queries.Dtos
{
    public class UserInfoProfile:Profile
    {
        public UserInfoProfile()
        {
            CreateMap<AppUser, UserInfoDto>();
        }
    }
}
