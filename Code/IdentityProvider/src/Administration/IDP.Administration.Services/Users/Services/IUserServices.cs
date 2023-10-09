using IDP.Administration.Services.Users.Dtos;
using Microsoft.AspNetCore.Identity;

namespace IDP.Administration.Services.Users.Services
{
    public interface IUserServices
    {
        public Task<(IdentityResult identityResult, CreatedUserDto? createdUser)> CreateUserAsync(CreateUserDto userDto,
            CancellationToken cancellationToken = default);
    }
}
