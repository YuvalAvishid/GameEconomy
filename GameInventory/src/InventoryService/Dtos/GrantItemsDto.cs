namespace InventoryService.Dtos;

public record GrantItemsDto(Guid UserId, Guid ProductItemId, int Quantity);
