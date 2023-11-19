using Framework.Presentation.AspNetCore.Contracts;

namespace Framework.Test.EntityFramework;

public class AuthenticatedUserStub : IAuthenticatedUser
{
    private string _sub;

    private string _userAgent;

    private string _userIp;

    private string _userName;

    private bool _isCurrentUser;

    public AuthenticatedUserStub(string sub, string userAgent, string userIp, string userName, bool isCurrentUser)
    {
        _sub = sub;

        _userAgent = userAgent;

        _userIp = userIp;

        _userName = userName;

        _isCurrentUser = isCurrentUser;
    }

    public string? GetSub()
    {
        return _sub;
    }

    public string? GetUserAgent()
    {
        return _userAgent;
    }

    public string? GetUserIp()
    {
        return _userIp;
    }

    public string? GetUsername()
    {
        return _userName;
    }

    public bool IsCurrentUser(string userId)
    {
        return _isCurrentUser;
    }
}
