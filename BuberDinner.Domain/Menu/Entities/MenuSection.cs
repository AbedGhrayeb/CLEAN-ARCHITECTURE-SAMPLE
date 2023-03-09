using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Menu.ValueObjects;

namespace BuberDinner.Domain.Menu.Entities
{
    public sealed class MenuSection : Entity<MenuSectionId>
    {
        private readonly List<MenuItems> _Items = new();
        public string Name { get; }
        public string Description { get; }
        public IReadOnlyList<MenuItems> Items => _Items.AsReadOnly();
        private MenuSection(MenuSectionId id, string name, string description) : base(id)
        {
            Name = name;
            Description = description;
        }
        public static MenuSection Create(string name, string description) => new MenuSection(MenuSectionId.CreateUnique(), name, description);

    }
}