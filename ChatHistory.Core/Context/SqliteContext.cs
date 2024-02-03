using ChatHistory.Core.Config;
using ChatHistory.Core.Models;
using Dapper;
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

    private object? FindOne(string sql)
    {
        Db.Open();
        var result = Db.QuerySingleOrDefault(sql);
        Db.Close();

        return result;
    }
    
    private IEnumerable<object> FindAll(string sql)
    {
        Db.Open();
        var result = Db.Query(sql);
        Db.Close();

        return result;
    }
    
    public User? GetUserById(int id)
    {
        /*Db.Open();
        var sql = $"SELECT * FROM table_users WHERE id = {id}";
        var user = Db.QueryFirstOrDefault<User>(sql);
        Db.Close();
        return user;*/
        var sql = $"SELECT * FROM table_users WHERE id = {id}";
        return FindOne(sql) as User;
    }

    public IEnumerable<User>? GetUsersByName(string name)
    {
        var sql = $"SELECT * FROM table_users WHERE name = '{name}'";
        var result = FindAll(sql);
        return !result.Any() ? null : result.Cast<User>();
    }

    public IEnumerable<User>? GetAllUsers()
    {
        var sql = "SELECT * FROM table_users";
        var result = FindAll(sql);
        return !result.Any() ? null : result.Cast<User>();
    }

    public Message? GetMessageById(int id)
    {
        var sql = $"SELECT * FROM table_history WHERE id = {id}";
        return FindOne(sql) as Message;
    }

    public IEnumerable<Message>? GetAllMessages()
    {
        var sql = "SELECT * FROM table_history";
        var result = FindAll(sql);
        return !result.Any() ? null : result.Cast<Message>();
    }
}