using FluentAssertions;
using IDP.Administration.Api.Test.Integration.Fixtures;
using IDP.Administration.Api.Test.Integration.TestBuilders.Users;
using IDP.Administration.Api.Test.Integration.TestData;
using IDP.Administration.Api.Users.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace IDP.Administration.Api.Test.Integration.TestClasses
{

    public class UsersApiTests : IdpWebApiTransactionRollbackTestBase
    {
        public UsersApiTests(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task create_user_api_should_return_created_http_status_code()
        {

            var createRequestBody = CreateUserRequestBodyBuilder.Instantiate().BuildRequest();

            var response = await Client.PostAsync(UsersTestData.Endpoints.Create, createRequestBody);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

        }

        [Fact]
        public async Task create_user_api_returns_user_info_on_success_request()
        {
            var expectedResponseBody = CreateUserResponseBodyBuilder
                .Instantiate()
                .WithEmail(UsersTestData.Sajad.Email)
                .WithEmailConfirmed(false)
                .WithPhoneNumber(UsersTestData.Sajad.PhoneNumber)
                .WithUserName(UsersTestData.Sajad.PhoneNumber)
                .Build();

            var createUserRequstBody = CreateUserRequestBodyBuilder
                .Instantiate()
                .WithEmail(UsersTestData.Sajad.Email)
                .WithPhoneNumber(UsersTestData.Sajad.PhoneNumber)
                .BuildRequest();

            var response = await Client.PostAsync(UsersTestData.Endpoints.Create, createUserRequstBody);

            var responseBody = await response.Content.ReadFromJsonAsync<CreateUserResponseBody>();

            responseBody.Should().BeEquivalentTo(expectedResponseBody, options => options.Excluding(ex => (ex.Id)));
            responseBody?.Id.Should().NotBeNullOrEmpty();

        }

      

    }


}
