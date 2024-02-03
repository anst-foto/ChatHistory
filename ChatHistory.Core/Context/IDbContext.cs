using ChatHistory.Core.Models;

namespace ChatHistory.Core.Context;

public interface IDbContext
{
    public User? GetUserById(int id);
    public IEnumerable<User>? GetUsersByName(string name);
    public IEnumerable<User>? GetAllUsers();

    public Message? GetMessageById(int id);
    public IEnumerable<Message>? GetAllMessages();
}
