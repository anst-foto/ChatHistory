namespace ChatHistory.Core.Models;

public record Message
{
    public required int Id { get; init; }
    public required User? Sender { get; init; }
    public required User? Receiver { get; init; }
    public required string MessageText { get; init; }
    public Message? ReplyMessage { get; init; }
    public required DateTime DateSend { get; init; }
    public required bool IsReceive { get; init; }
    public required bool IsRead { get; init; }
    public required bool IsDelete { get; init; }
}
