namespace IDP.Administration.ServiceHost.Helpers
{
    public static class EnvironmentHelper
    {
        public static string GetHostingEnvironment()
        {

            var x = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            var y = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return Environments.Development;
        }
    }
}
