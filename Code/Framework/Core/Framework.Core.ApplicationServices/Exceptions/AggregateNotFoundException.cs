namespace Framework.Core.ApplicationServices.Exceptions;
public class AggregateNotFoundException : ApplicationServicesException
{
    public AggregateNotFoundException(string message, string code, string name) : base(message, code, name)
    {
    }
}
