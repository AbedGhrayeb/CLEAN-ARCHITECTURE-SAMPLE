using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Host.ValueObjects
{
    public sealed class HostId : ValueObject
    {
        private HostId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public static HostId CreateUnique() => new HostId(Guid.NewGuid());


        public override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
