namespace Framework.Events.OutBox.Models;

public class OutBoxEventItem
{
    public long Id { get; set; }
    public required string EventId { get; set; }
    public string? OccurredByUserId { get; set; }
    public DateTimeOffset OccurredOn { get; set; }
    public string? AggregateName { get; set; }
    public string? AggregateTypeName { get; set; }
    public string? EventName { get; set; }
    public string? EventTypeName { get; set; }
    public string? EventPayload { get; set; }
}
