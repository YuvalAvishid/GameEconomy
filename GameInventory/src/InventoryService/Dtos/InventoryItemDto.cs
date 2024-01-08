namespace InventoryService.Dtos;

public record InventoryItemDto(Guid ProductItemId, int Quantity, DateTimeOffset AcquiredTime);
