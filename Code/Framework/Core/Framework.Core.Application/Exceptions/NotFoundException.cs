namespace Framework.Core.Application.Exceptions;
public class NotFoundException(string message, string code, string name)
    : ApplicationServicesException(message, code, name);