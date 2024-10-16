namespace Framework.Core.Domain.ValueObjects;

public class EntityId : ValueObject<EntityId>
{
    public static EntityId Instantiate(string value) => new(value);

    protected EntityId() { }

    protected EntityId(string value)
    {
        Value=value;
        Validate();
    }

    public string Value { get; set; } = null!;
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public sealed override void Validate()
    {
        ArgumentException.ThrowIfNullOrEmpty(Value);
    }

    public override string ToString()
    {
        return Value;
    }
}