using InventoryService.Dtos;
using InventoryService.Entities;

namespace InventoryService;

public static class InventoryItemExtensions
{
    public static InventoryItemDto AsDto(this InventoryItem inventoryItem, string name, string description)
    {
        return new InventoryItemDto(inventoryItem.ProductItemId, name, description, inventoryItem.Quantity, inventoryItem.AcquiredTime);
    }
}
