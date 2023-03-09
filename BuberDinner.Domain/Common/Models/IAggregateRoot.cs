namespace BuberDinner.Domain.Common.Models
{
    public interface IAggregateRoot<TId>
    {
        bool Equals(AggregateRoot<TId>? other);
        bool Equals(object? obj);
    }
}