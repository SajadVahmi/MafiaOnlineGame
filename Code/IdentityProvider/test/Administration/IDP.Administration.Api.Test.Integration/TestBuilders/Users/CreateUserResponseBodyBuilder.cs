using IDP.Administration.Api.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Administration.Api.Test.Integration.TestBuilders.Users
{
    public class CreateUserResponseBodyBuilder
    {
        private string _id;
        private string _userName;
        private string _mobile;
        private string _email;
        private bool _emailConfirmed;
        private bool _twoFactorEnabled;
        private string? _lockoutEnd;
        private bool _lockoutEnabled;

        public static CreateUserResponseBodyBuilder Instantiate() => new();

        protected CreateUserResponseBodyBuilder(){

            _id = "Not filled id";
        }

        public CreateUserResponseBodyBuilder WithId(string id)
        {
            _id = id;
            return this;
        }

        public CreateUserResponseBodyBuilder WithUserName(string userName)
        {
            _userName = userName;
            return this;
        }

        public CreateUserResponseBodyBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }
        public CreateUserResponseBodyBuilder WithEmailConfirmed(bool emailConfirmed)
        {
            _emailConfirmed = emailConfirmed;
            return this;
        }
        public CreateUserResponseBodyBuilder WithPhoneNumber(string mobile)
        {
            _mobile = mobile;
            return this;
        }

        public CreateUserResponseBodyBuilder WithLockoutEnabled(bool lockoutEnabled)
        {
            _lockoutEnabled = lockoutEnabled;
            return this;
        }
        public CreateUserResponseBodyBuilder WithLockoutEnd(string lockoutEnd)
        {
            _lockoutEnd = lockoutEnd;
            return this;
        }
        public CreateUserResponseBodyBuilder WithTwoFactorEnabled(bool twoFactorEnabled)
        {
            _twoFactorEnabled = twoFactorEnabled;
            return this;
        }

        public CreateUserResponseBody Build() => new()
        {
            Id=_id,
            UserName = _userName,
            Email = _email,
            EmailConfirmed = _emailConfirmed,
            PhoneNumber = _mobile,
            LockoutEnabled = _lockoutEnabled,
            LockoutEnd = _lockoutEnd,
            TwoFactorEnabled = _twoFactorEnabled,

        };


    }
}
