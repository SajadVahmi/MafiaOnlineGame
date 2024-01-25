namespace Framework.Core.ApplicationServices.Exceptions;
public class NotFoundException(string message, string code, string name)
    : ApplicationServicesException(message, code, name);
