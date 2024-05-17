namespace Framework.Core.Application.Exceptions;

public class ConflictException(string message, string code, string name)
    : ApplicationServicesException(message, code, name);