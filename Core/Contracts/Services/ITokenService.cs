using Application.Auth.Commands.Dtos;
using Domain.Entites.IdentityEntities;
using Infrastructure.IdentityEntities;

namespace Application.Contracts.Services
{
    public interface ITokenService
    {
         string CreateToken(AppUser user,IList<string>Roles);
         Task<AuthDto> RenewAccessTokenAsync(string token);
         RefreshToken CreateRefreshToken(AppUser user);

         Task<bool> RevokeRefreshTokenAsync(string refreshToken);
    }
}
