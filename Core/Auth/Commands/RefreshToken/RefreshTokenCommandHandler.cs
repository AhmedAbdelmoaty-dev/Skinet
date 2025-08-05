using Application.Auth.Commands.Dtos;
using Application.Contracts.Services;
using Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthDto>
    {
        private readonly ITokenService _tokenService;

        public RefreshTokenCommandHandler(ITokenService tokenService)
        {
           _tokenService = tokenService;
        }
        public async Task<AuthDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result= await _tokenService.RenewAccessTokenAsync(request.RefreshToken);
            if (result == null)
            {
                throw new BadRequestException("Failed to renew access token");
            }
            return result;
        }

    }
}
