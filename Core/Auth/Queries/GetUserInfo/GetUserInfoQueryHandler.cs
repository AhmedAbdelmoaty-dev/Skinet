using Application.Auth.Queries.Dtos;
using Application.Exceptions;
using Infrastructure.IdentityEntities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Skinet.Extensions;
using AutoMapper;

namespace Application.Auth.Queries.GetUserInfo
{
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserInfoDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public GetUserInfoQueryHandler(UserManager<AppUser> userManager,IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserInfoDto> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            if(!request.user.Identity.IsAuthenticated)
                throw new UnAuthorizedException("User is not authenticated");

            AppUser user = request.WithAddress ?
                await _userManager.GetUserByEmailWithAddress(request.user) :
                await _userManager.GetUserByEmail(request.user);


            return _mapper.Map<UserInfoDto>(user);
        }
    }
}
