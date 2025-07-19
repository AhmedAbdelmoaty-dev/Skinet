using Application.Auth.Commands.Dtos;
using Application.Contracts.Services;
using Application.Exceptions;
using Domain.Entites.IdentityEntities;
using Infrastructure.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _config;
        public TokenService(UserManager<AppUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;

        }


        public string CreateToken(AppUser user, IList<string> Roles)
        {
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.Name,user.UserName!),
                new Claim("firstname", user.FirstName!),
                new Claim("lastname", user.LastName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            foreach (var role in Roles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenConfiguration = new JwtSecurityToken
            (
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse (_config["JWT:DurationInMinutes"])),
                signingCredentials:credentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenConfiguration);
            return token;
        }


        public RefreshToken CreateRefreshToken(AppUser user)
        {

            var key = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(key);
            return new RefreshToken
            {
                Token = refreshToken,
                ExpiryTime = DateTime.UtcNow.AddDays(10),
                CreatedAt = DateTime.UtcNow
            };
        }
        public async Task<AuthDto> UpdateRefreshTokenAsync(string token)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.RefreshTokens.Any(x => x.Token == token));
            if (user == null)
            {
                throw new BadRequestException("Invalid token");
            }
            var oldRefreshToken = user.RefreshTokens.FirstOrDefault(x => x.Token == token);
            if (!oldRefreshToken.IsActive)
            {
                throw new BadRequestException("Invalid token");
            }
           oldRefreshToken.RevokedOn = DateTime.UtcNow;
            var newRefreshToken = CreateRefreshToken(user);
            user.RefreshTokens.Add(newRefreshToken);
            var result = await _userManager.UpdateAsync(user);
            var roles= await _userManager.GetRolesAsync(user);
            var accessToken = CreateToken(user, roles);
            return new AuthDto
            {
                Email = user.Email,
                UserName = user.UserName,
                UserId = user.Id,
                IsAuthenticated = true,
                Token = accessToken,
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpiration = newRefreshToken.ExpiryTime,
                Roles=roles
            };
        }

        public async Task<bool> RevokeRefreshTokenAsync(string token)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshTokens.Any(t => t.IsActive));
            if (user == null)
                return false;
            var refreshToken = user.RefreshTokens.First(c => c.Token == token);
            if (!refreshToken.IsActive)
                return false;
            refreshToken.RevokedOn = DateTime.UtcNow;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new BadRequestException(string.Join(",", result.Errors.Select(e => e.Description)));
            }
            return true;
        }
    }
}
