using InventoryService.Dtos;
using InventoryService.Entities;

namespace InventoryService;

public static class InventoryItemExtensions
{
    public static InventoryItemDto AsDto(this InventoryItem inventoryItem)
    {
        return new InventoryItemDto(inventoryItem.ProductItemId, inventoryItem.Quantity, inventoryItem.AcquiredTime);
    }
}
