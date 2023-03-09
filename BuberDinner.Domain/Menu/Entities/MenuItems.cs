using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Menu.ValueObjects;

namespace BuberDinner.Domain.Menu.Entities
{
    public sealed class MenuItems : Entity<MenuItemId>
    {
        public string Name { get; }
        public string Description { get; }

        private MenuItems(MenuItemId menuItemId, string name, string description) : base(menuItemId)
        {
            Name = name;
            Description = description;
        }
        public static MenuItems Create(string name, string description) =>
                new MenuItems(MenuItemId.CreateUnique(), name, description);
    }
}