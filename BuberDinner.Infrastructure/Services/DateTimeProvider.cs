using BuberDinner.Application.Common.Interfaces.Services;

namespace BuberDinner.Infrastructure.Services
{
    internal class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow() => DateTime.UtcNow;

    }
}
