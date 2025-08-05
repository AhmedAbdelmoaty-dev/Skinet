using AutoMapper;
using Infrastructure.IdentityEntities;


namespace Application.Auth.Queries.Dtos
{
    public class UserInfoProfile:Profile
    {
        public UserInfoProfile()
        {
            CreateMap<AppUser, UserInfoDto>()
            .ForMember(dest => dest.Roles, opt => opt.Ignore());

            CreateMap<Domain.Entites.Address, AddressDto>();
        }
    }
}
