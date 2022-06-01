using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestApp.Fundamentals.xUnitUnitTests
{
    public class InventoryTests
    {
        private Inventory inventory;

        public InventoryTests()
        {
            inventory = new Inventory();         
        }

        private List<InventoryItem> inventoryPriceItems => new List<InventoryItem>
        {
            new InventoryItem { Price = 10 },
            new InventoryItem { Price = 1 },
            new InventoryItem { Price = 5 },
            new InventoryItem { Price = 2 },
        };

        private List<InventoryItem> inventoryNameItems => new List<InventoryItem>
        {
            new InventoryItem { Name = "abc" },
            new InventoryItem { Name = "zxy" },
            new InventoryItem { Name = "cba" },
            new InventoryItem { Name = "yxz" },
        };

        [Fact]
        public void SortByPrice_OrderByLowToHigh_ShouldOrderedAscendingByPrice()
        {
            // Arrange
            inventory.InventoryItems = inventoryPriceItems;

            // Act
            inventory.SortByPrice(Order.Ascending);

            // Assert
            inventory.InventoryItems.Should().BeInAscendingOrder(p => p.Price);

        }

        [Fact]
        public void SortByPrice_OrderByHighToLow_ShouldOrderedDescendingByPrice()
        {
            // Arrange
            inventory.InventoryItems = inventoryPriceItems;

            // Act
            inventory.SortByPrice(Order.Descending);

            // Assert
            inventory.InventoryItems.Should().BeInDescendingOrder(p => p.Price);

        }

        [Fact]
        public void SortByName_OrderByLowToHigh_ShouldOrderedAscendingByName()
        {
            // Arrange
            inventory.InventoryItems = inventoryNameItems;

            // Act
            inventory.SortByName(Order.Ascending);

            // Assert
            inventory.InventoryItems.Should().BeInAscendingOrder(p => p.Name);
        }

        [Fact]
        public void SortByName_OrderByHighToLow_ShouldOrderedDescendingByName()
        {
            // Arrange
            inventory.InventoryItems = inventoryNameItems;

            // Act
            inventory.SortByName(Order.Descending);

            // Assert
            inventory.InventoryItems.Should().BeInDescendingOrder(p => p.Name);
        }
    }
}
