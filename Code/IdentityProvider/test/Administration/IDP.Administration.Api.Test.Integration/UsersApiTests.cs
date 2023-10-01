using FluentAssertions;
using IDP.Administration.Api.Test.Integration.Fixtures;
using IDP.Administration.Api.Users.Models;
using IDP.Shared.IdentityStore.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace IDP.Administration.Api.Test.Integration
{
    [Collection("IdpWebApp collection")]
    public class UsersApiTests 
    {
        private readonly HttpClient _client;
        private readonly IdpWebApplicationFactory<Program> _factory;
        private readonly IServiceScope _scope;
        private readonly IDbContextTransaction _transaction;

        public UsersApiTests(IdpWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
            _scope = factory.Services.CreateScope();

            var dbContext = _scope.ServiceProvider.GetRequiredService<IdpDbContext>();
            _transaction = dbContext.Database.BeginTransaction();
        }


        [Fact]
        public async Task create_user_api_call_should_returen_created_http_status_code()
        {
            // Arrange: Prepare the request payload
            var userToCreate = new CreateUserRequestBody
            {
                Email = "test@example.com",
                Mobile = "09387607524",
                OtpSmsEnabled = true,
                LockoutEnabled = true,
                TwoFactorEnabled = true,
            };
            var content = new StringContent(JsonConvert.SerializeObject(userToCreate), Encoding.UTF8, "application/json");

            // Act: Make an HTTP POST request to create a user
            var response = await _client.PostAsync("/v1/users", content);

            // Assert: Check the response
            response.StatusCode.Should().Be(HttpStatusCode.Created);

        }

        public void Dispose()
        {
            _transaction.Rollback();
            _transaction.Dispose();
            _scope.Dispose();
        }
    }


}
