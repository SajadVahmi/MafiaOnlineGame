using System.Globalization;
using System.Resources;

namespace Framework.Core.Domain.Exceptions;

public class BusinessException(string message, string code, string name) : Exception(message)
{
    public string Code { get; private set; } = code;
    public string Name { get; private set; } = name;

    public string? GetLocalizedMessage(ResourceManager resourceManager, CultureInfo cultureInfo)
    {
        return resourceManager.GetString(Name, cultureInfo);
    }
}
