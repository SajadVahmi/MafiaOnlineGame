
namespace Framework.Core.Contracts;

public interface IAuthenticatedUser
{
    string? GetUserAgent();

    string? GetUserIp();

    string? GetSub();

    string? GetUsername();

    bool IsCurrentUser(string userId);

}
