namespace Framework.Core.Domian.Events
{
    public interface IEvent
    {
        string EventId { get; }
        DateTimeOffset WhenItHappened { get; }
    }
}
