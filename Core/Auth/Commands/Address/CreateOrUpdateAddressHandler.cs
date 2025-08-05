using Application.Auth.Commands.Dtos;
using AutoMapper;
using Infrastructure.IdentityEntities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Skinet.Extensions;

namespace Application.Auth.Commands.Address
{
    public class CreateOrUpdateAddressHandler : IRequestHandler<CreateOrUpdateAddressCommand, AddressDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _user;
        public CreateOrUpdateAddressHandler(UserManager<AppUser> userManager,IMapper mapper,IHttpContextAccessor user)
        {
            _userManager = userManager;
            _mapper = mapper;
            _user = user;
        }
        public async Task<AddressDto> Handle(CreateOrUpdateAddressCommand request, CancellationToken cancellationToken)
        {
            if(_user.HttpContext.User.Identity.IsAuthenticated == false)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var user=  await _userManager.GetUserByEmailWithAddress(_user.HttpContext.User);
            if (user.Address == null)
            {
                user.Address = new Domain.Entites.Address 
                {
                    PostalCode = request.PostalCode,
                    Line1 = request.Line1,
                    Line2 = request.Line2,
                    City = request.City,
                    State = request.State,
                    Country = request.Country
                };

            }
            else
            {
                user.Address.PostalCode = request.PostalCode;
                user.Address.Line1 = request.Line1;
                user.Address.Line2 = request.Line2;
                user.Address.City = request.City;
                user.Address.State = request.State;
                user.Address.Country = request.Country;
            }
                var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
              var errors= string.Join(",",result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to update address , {errors}");
            }

            return _mapper.Map<AddressDto>(user.Address);
        }
    }
}
