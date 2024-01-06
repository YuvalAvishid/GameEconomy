namespace ProductService;

public record ItemDto(Guid Id, string Name, string Description, decimal Price, DateTimeOffset CreatedDate);