

using Application.Contracts.Services;
using Application.Exceptions;
using MediatR;

namespace Application.Auth.Commands.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
    {
        private readonly ITokenService _tokenService;

        public LogoutCommandHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
          var result=await  _tokenService.RevokeRefreshTokenAsync(request.refreshToken);
            if (!result)
                throw new BadRequestException("Something went wrong");
            return;
        }
    }
}
