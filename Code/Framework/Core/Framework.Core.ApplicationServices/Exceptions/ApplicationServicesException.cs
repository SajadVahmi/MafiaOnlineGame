using System.Globalization;
using System.Resources;

namespace Framework.Core.ApplicationServices.Exceptions;

public class ApplicationServicesException : Exception
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public ApplicationServicesException(string message, string code, string name) : base(message)
    {
        Code = code;
        Name = name;
    }

    public string? GetLocalizedMessage(ResourceManager resourceManager, CultureInfo cultureInfo)
    {
        return resourceManager.GetString(Name, cultureInfo);
    }
}
