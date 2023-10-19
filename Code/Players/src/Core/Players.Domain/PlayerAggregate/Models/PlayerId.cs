using Framework.Core.Domian.ValueObjects;

namespace Players.Domain.PlayerAggregate.Models
{
    public class PlayerId : ValueObject<PlayerId>
    {
        public static PlayerId Instantiate(long value) => new(value);

        protected PlayerId(long value) =>
            Value = value;

        public long Value { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
