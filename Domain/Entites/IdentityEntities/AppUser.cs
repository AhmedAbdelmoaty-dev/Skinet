using Domain.Entites;
using Domain.Entites.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.IdentityEntities
{
    public class AppUser:IdentityUser
    {
        [StringLength(50,MinimumLength =3)]
        public string? FirstName { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string? LastName { get; set; }

        public Address? Address { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; } = new();

    }
}
