namespace InventoryService.Dtos;

public record InventoryItemDto(Guid ProductItemId, string Name, string Description, int Quantity, DateTimeOffset AcquiredTime);
