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
      

        public static CreateUserRequestBodyBuilder Instantiate() => new();
        protected CreateUserRequestBodyBuilder()
        {
            _email = UsersTestData.Somebody.Email;
            _mobile = UsersTestData.Somebody.PhoneNumber;
           
        }

        public CreateUserRequestBodyBuilder WithEmail(string? email)
        {
            _email = email;
            return this;
        }
        public CreateUserRequestBodyBuilder WithPhoneNumber(string? mobile)
        {
            _mobile = mobile;
            return this;
        }

        public CreateUserRequestBody Build()
        {
          
            return new CreateUserRequestBody()
            {
                Email = _email,
                Mobile = _mobile
            };
        }

        public StringContent BuildRequest()
        {
            return new StringContent(JsonConvert.SerializeObject(Build()), Encoding.UTF8, "application/json");
        }
    }
}
