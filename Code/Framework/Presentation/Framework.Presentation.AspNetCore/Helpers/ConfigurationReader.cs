using Microsoft.Extensions.Configuration;

namespace Framework.Presentation.AspNetCore.Helpers;

public static class ConfigurationReader
{
    public static TConfiguration? Read<TConfiguration>(string fileName, string key)
    {
        IConfiguration Configuration =
         new ConfigurationBuilder()
             .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
             .AddJsonFile(fileName, optional: false, reloadOnChange: true)
             .Build();

        return Configuration.GetSection(key).Get<TConfiguration>();
    }

}
