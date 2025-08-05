
namespace Application.Auth.Queries.Dtos
{
    public class UserInfoDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public IList<string>? Roles { get; set; }
        public AddressDto? Address { get; set; }
    }
}
