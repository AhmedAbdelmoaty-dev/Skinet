using Application.Auth.Commands.Dtos;
using Domain.Entites.IdentityEntities;
using Infrastructure.IdentityEntities;

namespace Application.Contracts.Services
{
    public interface ITokenService
    {
        public string CreateToken(AppUser user,IList<string>Roles);
        public Task<AuthDto> UpdateRefreshTokenAsync(string token);
        public RefreshToken CreateRefreshToken(AppUser user);

        public Task<bool> RevokeRefreshTokenAsync(string refreshToken);
    }
}
