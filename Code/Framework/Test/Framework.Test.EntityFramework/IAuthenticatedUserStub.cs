using Framework.Core.Contracts;

namespace Framework.Test.EntityFramework;

public class AuthenticatedUserStub(string sub, string userAgent, string userIp, string userName, bool isCurrentUser)
    : IAuthenticatedUser
{
    public string GetSub() => sub;

    public string GetUserAgent() => userAgent;

    public string GetUserIp() => userIp;

    public string GetUsername() => userName;

    public bool IsCurrentUser(string userId) => isCurrentUser;
}
