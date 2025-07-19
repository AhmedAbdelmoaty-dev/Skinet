
using Application.Auth.Commands.Register;
using AutoMapper;
using Infrastructure.IdentityEntities;

namespace Application.Auth.Commands.Dtos
{
    internal class AuthProfile:Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterCommand, AppUser>();
                
        }
    }
}
