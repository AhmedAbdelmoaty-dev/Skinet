using Microsoft.EntityFrameworkCore;

namespace Domain.Entites.IdentityEntities
{
    [Owned]
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpiryTime { get; set; }
        public bool IsExipired { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? RevokedOn { get; set; }
        public bool IsActive => RevokedOn == null && !IsExipired;
    }
}
