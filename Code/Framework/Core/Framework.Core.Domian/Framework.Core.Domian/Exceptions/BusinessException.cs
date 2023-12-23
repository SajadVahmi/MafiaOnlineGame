using System.Globalization;
using System.Resources;

namespace Framework.Core.Domian.Exceptions;

public class BusinessException:Exception
{
    public string Code { get;private set; }
    public string Name { get; private set; }
    public BusinessException(string message,string code, string name) : base(message)
    {
        Code = code;
        Name = name;
    }

    public string? GetLocalizedMessage(ResourceManager resourceManager, CultureInfo cultureInfo)
    {
        return resourceManager.GetString(Name, cultureInfo);
    }
}
