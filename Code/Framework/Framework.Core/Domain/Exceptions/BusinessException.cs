using System.Globalization;
using System.Resources;

namespace Framework.Core.Domain.Exceptions;

public class BusinessException(string message) : Exception(message)
{
    public string? GetLocalizedMessage(ResourceManager resourceManager, CultureInfo cultureInfo)
    {
        return resourceManager.GetString(Message, cultureInfo);
    }
}
