using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Common.ValueObjects;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu.Entities;
using BuberDinner.Domain.Menu.ValueObjects;
using BuberDinner.Domain.MenuReview.ValueObjects;

namespace BuberDinner.Domain.Menu
{
    public sealed class Menu : AggregateRoot<MenuId>
    {
        private readonly List<MenuSection> _sections = new();
        private readonly List<DinnerId> _dinnerIds = new();
        private readonly List<MenuReviewId> _menuReviweIds = new();
        private Menu(MenuId menuId, string name, string description, DateTime createDateTime, DateTime updateDateTime, HostId hostId, AverageRating averageRating) : base(menuId)
        {
            Name = name;
            Description = description;
            CreateDateTime = createDateTime;
            UpdateDateTime = updateDateTime;
            HostId = hostId;
        }

        public static Menu Create(string name, string Description, HostId hostId, AverageRating averageRating)
        {
            return new Menu(MenuId.CreateUnique(), name, Description, DateTime.UtcNow, DateTime.UtcNow, hostId, averageRating);
        }

        public string Name { get; }
        public string Description { get; }
        public AverageRating AverageRating { get; }
        public DateTime CreateDateTime { get; }
        public DateTime UpdateDateTime { get; }
        public HostId HostId { get; }
        public IReadOnlyList<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();
        public IReadOnlyList<MenuReviewId> MenuReviewIds => _menuReviweIds.AsReadOnly();
        public IReadOnlyList<MenuSection> Sections => _sections.AsReadOnly();

    }
}
