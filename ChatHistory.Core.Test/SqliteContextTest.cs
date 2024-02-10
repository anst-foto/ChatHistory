using ChatHistory.Core.Config;
using ChatHistory.Core.Context;
using ChatHistory.Core.Models;

namespace ChatHistory.Core.Test;

public class SqliteContextTest
{
    private readonly SqliteContext context;
    private readonly List<User> users;
    private readonly List<Message> messages;

    public SqliteContextTest()
    {
        this.context = new SqliteContext();
        this.users = new List<User>()
        {
            new() { Id = 1, Name = "user008", IsDelete = false },
            new() { Id = 2, Name = "sa", IsDelete = false },
            new() { Id = 3, Name = "qwerty", IsDelete = false }
        };

        this.messages = new List<Message>()
        {
            new() {Id = 1, Sender = this.users[0], Receiver = this.users[1], MessageText = "test", ReplyMessage = null, DateSend = new DateTime(2024, 1, 27, 0,0,0, DateTimeKind.Utc), IsReceive = false, IsRead = false, IsDelete = false},
            new() {Id = 2, Sender = this.users[0], Receiver = this.users[1], MessageText = "test", ReplyMessage = null, DateSend = new DateTime(2024, 1, 27, 0,0,0, DateTimeKind.Utc), IsReceive = false, IsRead = false, IsDelete = false},
            new() {Id = 3, Sender = this.users[0], Receiver = this.users[1], MessageText = "test", ReplyMessage = null, DateSend = new DateTime(2024, 1, 27, 0,0,0, DateTimeKind.Utc), IsReceive = false, IsRead = false, IsDelete = false}
        };
        this.messages.Add(new Message
        {
            Id = 4,
            Sender = this.users[2],
            Receiver = this.users[1],
            MessageText = "test",
            ReplyMessage = this.messages[0],
            DateSend = new DateTime(2024, 1, 27, 0,0,0, DateTimeKind.Utc),
            IsReceive = false,
            IsRead = false,
            IsDelete = false
        });
        this.messages.Add(new Message
        {
            Id = 5,
            Sender = this.users[0],
            Receiver = this.users[2],
            MessageText = "test",
            ReplyMessage = this.messages[1],
            DateSend = new DateTime(2024, 1, 27, 0,0,0, DateTimeKind.Utc),
            IsReceive = false,
            IsRead = false,
            IsDelete = false
        });
        this.messages.Add(new Message
        {
            Id = 6,
            Sender = this.users[2],
            Receiver = this.users[1],
            MessageText = "test",
            ReplyMessage = this.messages[0],
            DateSend = new DateTime(2024, 1, 27, 0,0,0, DateTimeKind.Utc),
            IsReceive = false,
            IsRead = false,
            IsDelete = false
        });
    }

    [Fact]
    public void SqliteContextConstructorTestException() =>
        Assert.Throws<ConfigException>(() =>
        {
            new SqliteContext("db_config_bad.json");
        } );

    [Fact]
    public void SqliteContextConstructorTest()
    {
        var actual = new SqliteContext();
        Assert.NotEqual(this.context, actual);
    }

    [Fact]
    public void GetUserByIdTest()
    {
        var expected = this.users[0];
        var actual = this.context.GetUserById(1)!;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetUserByIdTestNegative()
    {
        var actual = this.context.GetUserById(1000);
        Assert.Null(actual);
    }

    [Fact]
    public void GetUsersByNameTest()
    {
        var expected = this.users.Where(u => u.Name == "user008");
        var actual = this.context.GetUsersByName("user008")!;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetUsersByNameTestNegative()
    {
        var actual = this.context.GetUsersByName("");
        Assert.Null(actual);
    }

    [Fact]
    public void GetAllUsersTest()
    {
        var actual = this.context.GetAllUsers()!.ToList();
        Assert.Equal(this.users, actual);
    }

    [Fact]
    public void GetMessageByIdTest()
    {
        var expected = this.messages[0];
        var actual = this.context.GetMessageById(1)!;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetMessageByIdTestNegative()
    {
        var actual = this.context.GetMessageById(1000);
        Assert.Null(actual);
    }

    [Fact]
    public void GetAllMessagesTest()
    {
        var actual = this.context.GetAllMessages()!.ToList();
        Assert.Equal(this.messages, actual!);
    }

}
