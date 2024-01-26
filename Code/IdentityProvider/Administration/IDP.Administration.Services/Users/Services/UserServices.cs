using AutoMapper;
using IDP.Administration.Services.Users.Dto;
using IDP.Shared.IdentityStore.Models;
using Microsoft.AspNetCore.Identity;

namespace IDP.Administration.Services.Users.Services;

public class UserServices(UserManager<IdpUser> userManager, IMapper mapper) : IUserServices
{
    public async Task<(IdentityResult identityResult, string userId)> CreateUserAsync(CreateUserDto userDto, CancellationToken cancellationToken = default)
    {
        var idpUser = mapper.Map<IdpUser>(userDto);

        //TODO:Change it with distributed unique id
        idpUser.Id = DateTime.Now.Ticks.ToString();

        var identityResult = await userManager.CreateAsync(idpUser);

        return (identityResult, idpUser.Id);
    }
}