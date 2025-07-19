using Application.Exceptions;
using Infrastructure.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Skinet.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static async Task<AppUser> GetUserByEmail(this UserManager<AppUser> userManager,ClaimsPrincipal user)
        {
            var userToReturn=await userManager.FindByEmailAsync(user.GetEmail());
            if (userToReturn == null)
            {
                throw new UnAuthorizedException("User not found");
            }
            return userToReturn;

        }
        public static async Task<AppUser> GetUserByEmailWithAddress(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var userToReturn = await userManager.Users.Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Email == user.GetEmail());
            if (userToReturn == null)
            {
                throw new UnAuthorizedException("User not found");
            }
            return userToReturn;
        }


        public static string GetEmail(this ClaimsPrincipal user)
        {
          var email= user.FindFirstValue(ClaimTypes.Email) ??
                throw new UnAuthorizedException("Email claim not found");

            return email;
        }
    }
}
