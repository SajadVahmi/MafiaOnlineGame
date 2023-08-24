namespace IDP.Administration.Services.Models.UserServices
{
    public class CreateUserModel
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool LockoutEnabled { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool OtpSmsEnabled { get; set; }

    }
}
