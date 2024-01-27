using ChatHistory.Core.Config;
using ChatHistory.Core.Models;
using Microsoft.Data.Sqlite;

namespace ChatHistory.Core.Context;

public class SqliteContext : DbContext, IDbContext
{
    public SqliteContext() : base(new SqliteConfig(), new SqliteConnection())
    {
        var connectionString = SqliteConfig.GetFromJson()?.ToString();
        
        if (connectionString is null) throw new NullReferenceException();

        Db.ConnectionString = connectionString;
    }
    
    
    public User? GetUserById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User>? GetUsersByName(string name)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User>? GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public Message? GetMessageById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User>? GetAllMessages()
    {
        throw new NotImplementedException();
    }
}