namespace Framework.Presentation.AspNetCore.Constants;

public class RunningEnvironmentNames
{
    public static readonly string Development = Microsoft.Extensions.Hosting.Environments.Development;
    public static readonly string Staging = Microsoft.Extensions.Hosting.Environments.Staging;
    public static readonly string Production = Microsoft.Extensions.Hosting.Environments.Production;
    public static readonly string IntegrationTest = "IntegrationTest";
}
