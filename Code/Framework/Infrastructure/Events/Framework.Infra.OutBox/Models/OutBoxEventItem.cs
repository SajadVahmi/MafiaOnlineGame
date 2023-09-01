namespace Framework.Infra.OutBox.Models;

public class OutBoxEventItem
{
    public long Id { get; set; }
    public Guid EventId { get; set; }
    public string? AccuredByUserId { get; set; }
    public DateTimeOffset AccuredOn { get; set; }
    public string? AggregateName { get; set; }
    public string? AggregateTypeName { get; set; }
    public string? EventName { get; set; }
    public string? EventTypeName { get; set; }
    public string? EventPayload { get; set; }
}