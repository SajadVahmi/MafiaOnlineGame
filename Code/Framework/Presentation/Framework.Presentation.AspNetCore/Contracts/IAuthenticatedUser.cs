
namespace Framework.Presentation.AspNetCore.Contracts;

public interface IAuthenticatedUser
{
    string? GetUserAgent();

    string? GetUserIp();

    string? GetSub();

    string? GetUsername();

    bool IsCurrentUser(string userId);

}
