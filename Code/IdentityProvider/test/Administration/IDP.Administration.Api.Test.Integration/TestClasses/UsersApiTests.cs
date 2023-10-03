using FluentAssertions;
using IDP.Administration.Api.Test.Integration.Fixtures;
using IDP.Administration.Api.Test.Integration.TestBuilders.Users;
using IDP.Administration.Api.Test.Integration.TestData;
using IDP.Administration.Api.Users.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace IDP.Administration.Api.Test.Integration.TestClasses
{

    public class UsersApiTests : IdpWebApiTransactionRollbackTestBase
    {
        public UsersApiTests(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task create_user_api_call_should_return_created_http_status_code()
        {

            var createRequestBody = CreateUserRequestBodyBuilder.Instantiate().BuildRequest();
            
            var response = await Client.PostAsync(UsersTestData.Endpoints.Create, createRequestBody);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

        }


    }


}
