using IDP.Administration.Services.Users.Dto;
using Microsoft.AspNetCore.Identity;

namespace IDP.Administration.Services.Users.Services
{
    public interface IUserServices
    {
        public Task<(IdentityResult identityResult, string userId)> CreateUserAsync(CreateUserDto userDto,
            CancellationToken cancellationToken = default);
    }
}
