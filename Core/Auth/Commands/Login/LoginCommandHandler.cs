
using Application.Auth.Commands.Dtos;
using Application.Contracts.Services;
using Application.Exceptions;
using AutoMapper;
using Infrastructure.IdentityEntities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        public LoginCommandHandler(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<AuthDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new NotFoundException("Invalid UserName or password");
               
            }
            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                throw new NotFoundException("Invalid UserName or password");
            }

            var roles = await _userManager.GetRolesAsync(user);
            
            var token =_tokenService.CreateToken(user, roles);

            var refreshToken = _tokenService.CreateRefreshToken(user);

            return new AuthDto
            {
                Token = token,
                RefreshToken = refreshToken.Token,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles.ToList(),
                RefreshTokenExpiration = refreshToken.ExpiryTime,
                UserId = user.Id,
                IsAuthenticated = true
            };

        }
    }
}
