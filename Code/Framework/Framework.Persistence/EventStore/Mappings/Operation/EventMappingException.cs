namespace Framework.Persistence.EventStore.Mappings.Operation;

public class EventMappingException : Exception
{
    public EventMappingException(string message) : base(message)
    {
    }
}