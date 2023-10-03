using IDP.Administration.Api.Test.Integration.TestData;
using IDP.Administration.Api.Users.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Text;

namespace IDP.Administration.Api.Test.Integration.TestBuilders.Users
{
    public class CreateUserRequestBodyBuilder
    {
        private string? _email;
        private string? _mobile;
        private bool? _lockoutEnabled;
        private bool? _twoFactorEnabled;
        private bool? _otpSmsEnabled;

        public static CreateUserRequestBodyBuilder Instantiate() => new();
        public CreateUserRequestBodyBuilder()
        {
            _email = UsersTestData.Somebody.Email;
            _mobile = UsersTestData.Somebody.Mobile;
            _lockoutEnabled = UsersTestData.Somebody.LockoutEnabled;
            _otpSmsEnabled = UsersTestData.Somebody.OtpSmsEnabled;
            _twoFactorEnabled = UsersTestData.Somebody.TwoFactorEnabled;
        }

        public CreateUserRequestBodyBuilder WithEmail(string? email)
        {
            _email = email;
            return this;
        }
        public CreateUserRequestBodyBuilder WithMobile(string? mobile)
        {
            _mobile = mobile;
            return this;
        }

        public CreateUserRequestBodyBuilder WithLockoutEnabled(bool? lockoutEnabled)
        {
            _lockoutEnabled = lockoutEnabled;
            return this;
        }

        public CreateUserRequestBodyBuilder WithTwoFactorEnabled(bool? twoFactorEnabled)
        {
            _twoFactorEnabled = twoFactorEnabled;
            return this;
        }

        public CreateUserRequestBodyBuilder WithOtpSmsEnabled(bool? otpSmsEnabled)
        {
            _otpSmsEnabled = otpSmsEnabled;
            return this;
        }

        public CreateUserRequestBody Build()
        {
          
            return new CreateUserRequestBody()
            {
                Email = _email,
                Mobile = _mobile,
                LockoutEnabled = _lockoutEnabled,
                TwoFactorEnabled = _twoFactorEnabled,
                OtpSmsEnabled = _otpSmsEnabled
            };
        }

        public StringContent BuildRequest()
        {
            return new StringContent(JsonConvert.SerializeObject(Build()), Encoding.UTF8, "application/json");
        }
    }
}
