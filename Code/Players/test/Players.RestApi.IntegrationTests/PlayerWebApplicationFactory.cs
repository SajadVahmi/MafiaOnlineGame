using Framework.Test.Api.Fixtures;

namespace Players.RestApi.IntegrationTests;


[CollectionDefinition("application collection")]
public class WebApplicationCollection : ICollectionFixture<FrameworkWebApplicationFactory<Program>>;
