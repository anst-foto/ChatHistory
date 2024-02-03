namespace ChatHistory.Core.Models;

public record User
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required bool IsDelete { get; init; }
}
