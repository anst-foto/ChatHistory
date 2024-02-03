using ChatHistory.Core.Config;
using ChatHistory.Core.Context;
using ChatHistory.Core.Models;

namespace ChatHistory.Core.Test;

public class SqliteContextTest
{
    private readonly SqliteContext _context;
    private readonly List<User> _users;
    private readonly List<Message> _messages;

    public SqliteContextTest()
    {
        this._context = new SqliteContext();
        this._users = new List<User>()
        {
            new() { Id = 1, Name = "user008", IsDelete = false },
            new() { Id = 2, Name = "sa", IsDelete = false },
            new() { Id = 3, Name = "qwerty", IsDelete = false }
        };

        this._messages = new List<Message>()
        {
            new() {Id = 1, Sender = this._users[0], Receiver = this._users[1], MessageText = "test", ReplyMessage = null, DateSend = new DateTime(2024, 1, 27), IsReceive = false, IsRead = false, IsDelete = false},
            new() {Id = 2, Sender = this._users[0], Receiver = this._users[1], MessageText = "test", ReplyMessage = null, DateSend = new DateTime(2024, 1, 27), IsReceive = false, IsRead = false, IsDelete = false},
            new() {Id = 3, Sender = this._users[0], Receiver = this._users[1], MessageText = "test", ReplyMessage = null, DateSend = new DateTime(2024, 1, 27), IsReceive = false, IsRead = false, IsDelete = false}
        };
        this._messages.Add(new Message
        {
            Id = 4,
            Sender = this._users[2],
            Receiver = this._users[1],
            MessageText = "test",
            ReplyMessage = this._messages[0],
            DateSend = new DateTime(2024, 1, 27),
            IsReceive = false,
            IsRead = false,
            IsDelete = false
        });
        this._messages.Add(new Message
        {
            Id = 5,
            Sender = this._users[0],
            Receiver = this._users[2],
            MessageText = "test",
            ReplyMessage = this._messages[1],
            DateSend = new DateTime(2024, 1, 27),
            IsReceive = false,
            IsRead = false,
            IsDelete = false
        });
        this._messages.Add(new Message
        {
            Id = 6,
            Sender = this._users[2],
            Receiver = this._users[1],
            MessageText = "test",
            ReplyMessage = this._messages[0],
            DateSend = new DateTime(2024, 1, 27),
            IsReceive = false,
            IsRead = false,
            IsDelete = false
        });
    }

    [Fact]
    public void SqliteContextConstructorTestException() =>
        Assert.Throws<ConfigException>(() =>
        {
            var _ = new SqliteContext("db_config_bad.json");
        } );

    [Fact]
    public void SqliteContextConstructorTest()
    {
        var expected = this._context;
        var actual = new SqliteContext();
        Assert.NotEqual(expected, actual);
    }

    [Fact]
    public void GetUserByIdTest()
    {
        var expected = this._users[0];
        var actual = this._context.GetUserById(1)!;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetUserByIdTestNegative()
    {
        var actual = this._context.GetUserById(1000);
        Assert.Null(actual);
    }

    [Fact]
    public void GetUsersByNameTest()
    {
        var expected = this._users.Where(u => u.Name == "user008");
        var actual = this._context.GetUsersByName("user008")!;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetUsersByNameTestNegative()
    {
        var actual = this._context.GetUsersByName("");
        Assert.Null(actual);
    }

    [Fact]
    public void GetAllUsersTest()
    {
        var expected = this._users;
        var actual = this._context.GetAllUsers()!.ToList();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetMessageByIdTest()
    {
        var expected = this._messages[0];
        var actual = this._context.GetMessageById(1)!;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetMessageByIdTestNegative()
    {
        var actual = this._context.GetMessageById(1000);
        Assert.Null(actual);
    }

    [Fact]
    public void GetAllMessagesTest()
    {
        var expected = this._messages;
        var actual = this._context.GetAllMessages()!.ToList();
        Assert.Equal(expected, actual);
    }
}
