using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Dinner.ValueObjects
{
    public sealed class DinnerId : ValueObject
    {
        public DinnerId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public static DinnerId CreateUnique() => new DinnerId(Guid.NewGuid());
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
