namespace IDP.Administration.Api.V1.Users.Models;

public class CreateUserRequestBody
{
    public string? Email { get; set; }
    public string? Mobile { get; set; }
    public bool? LockoutEnabled { get; set; }
    public bool? TwoFactorEnabled { get; set; }
    public bool? OtpSmsEnabled { get; set; }
}