using AutoMapper;
using IDP.Administration.Services.Users.Dtos;
using IDP.Shared.IdentityStore.Models;
using Microsoft.AspNetCore.Identity;

namespace IDP.Administration.Services.Users.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<IdpUser> _userManager;
        private readonly IMapper _mapper;

        public UserServices(UserManager<IdpUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<(IdentityResult identityResult, string userId)> CreateUserAsync(CreateUserDto userDto, CancellationToken cancellationToken = default)
        {
            var idpUser = _mapper.Map<IdpUser>(userDto);

            //TODO:Change it with distributed unique id
            idpUser.Id = DateTime.Now.Ticks.ToString();

            var identityResult = await _userManager.CreateAsync(idpUser);

            return (identityResult, idpUser.Id);
        }
    }
}
