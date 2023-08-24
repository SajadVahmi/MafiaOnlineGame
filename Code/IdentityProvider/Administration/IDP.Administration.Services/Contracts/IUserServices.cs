using IDP.Administration.Services.Models.UserServices;
using Microsoft.AspNetCore.Identity;

namespace IDP.Administration.Services.Contracts
{
    public interface IUserServices
    {
        public Task<(IdentityResult identityResult, string userId)> CreateUserAsync(CreateUserModel userModel,
            CancellationToken cancellationToken = default);
    }
}
