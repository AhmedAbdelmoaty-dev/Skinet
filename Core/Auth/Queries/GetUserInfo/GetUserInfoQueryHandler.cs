using Application.Auth.Queries.Dtos;
using Application.Exceptions;
using Infrastructure.IdentityEntities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Skinet.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace Application.Auth.Queries.GetUserInfo
{
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserInfoDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        public GetUserInfoQueryHandler(UserManager<AppUser> userManager,IMapper mapper,IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<UserInfoDto> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            if(!_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
                throw new UnAuthorizedException("User is not authenticated");

            AppUser user = request.WithAddress ?
                await _userManager.GetUserByEmailWithAddress(_contextAccessor.HttpContext.User) :
                await _userManager.GetUserByEmail(_contextAccessor.HttpContext.User);


            return _mapper.Map<UserInfoDto>(user);
        }
    }
}
