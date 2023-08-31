namespace Framework.Core.ServiceContracts;

public interface IAuthenticatedUser
{
    string GetUserAgent();
    string GetUserIp();
    string UserId();

    string GetFirstName();
    string GetLastName();
    string GetUsername();
    bool IsCurrentUser(string userId);
    bool HasAccess(string accessKey);
}