using GameCommon.Entities;

namespace InventoryService.Entities;

public record InventoryItem : IEntity
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public Guid ProductItemId { get; init; }
    public int Quantity { get; init; }
    public DateTimeOffset AcquiredTime { get; init; }
}
