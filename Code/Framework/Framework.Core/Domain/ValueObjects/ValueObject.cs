namespace Framework.Core.Domain.ValueObjects;

public abstract class ValueObject<TValueObject> : IEquatable<TValueObject>
        where TValueObject : ValueObject<TValueObject>
{
    public bool Equals(TValueObject? other) => this == other;

    public override bool Equals(object? obj)
    {
        if (obj is TValueObject otherObject)
        {
            return GetEqualityComponents().SequenceEqual(otherObject.GetEqualityComponents());
        }
        return false;
    }
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x.GetHashCode())
            .Aggregate((x, y) => x ^ y);
    }
    protected abstract IEnumerable<object> GetEqualityComponents();
    public static bool operator ==(ValueObject<TValueObject>? right, ValueObject<TValueObject>? left)
    {
        if (right is null && left is null)
            return true;
        if (right is null || left is null)
            return false;
        return right.Equals(left);
    }
    public static bool operator !=(ValueObject<TValueObject> right, ValueObject<TValueObject> left) => !(right == left);

    public abstract void Validate();
}
