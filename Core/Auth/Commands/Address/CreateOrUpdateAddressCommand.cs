
using Application.Auth.Commands.Dtos;
using MediatR;
using System.Security.Claims;

namespace Application.Auth.Commands.Address
{
    public class CreateOrUpdateAddressCommand:IRequest<AddressDto>
    {
        public  string Line1 { get; set; }
        public string? Line2 { get; set; }
        public  string City { get; set; }
        public  string State { get; set; }
        public  string PostalCode { get; set; }
        public  string Country { get; set; }
    }
}
