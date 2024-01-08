namespace GameCommon.Settings;

public record RabbitMQSettings
{
    public string Host { get; init; } = string.Empty;
}
