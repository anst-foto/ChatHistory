using ChatHistory.Core.Config;
using ChatHistory.Core.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace ChatHistory.Core.Context;

public class SqliteContext : DbContext, IDbContext
{
    public SqliteContext() : base(new SqliteConfig(), new SqliteConnection())
    {
        var connectionString = SqliteConfig.GetFromJson()?.ToString() ?? throw new NullReferenceException();

        this.Db.ConnectionString = connectionString;
    }

    public User? GetUserById(int id)
    {
        /*Db.Open();
        var sql = $"SELECT * FROM table_users WHERE id = {id}";
        var user = Db.QueryFirstOrDefault<User>(sql);
        Db.Close();
        return user;*/
        var sql = $"SELECT * FROM table_users WHERE id = {id}";
        return this.FindOne(sql) as User;
    }

    public IEnumerable<User>? GetUsersByName(string name)
    {
        var sql = $"SELECT * FROM table_users WHERE name = '{name}'";
        var result = this.FindAll(sql);
        return !result.Any() ? null : result.Cast<User>();
    }

    public IEnumerable<User>? GetAllUsers()
    {
        var sql = "SELECT * FROM table_users";
        var result = this.FindAll(sql);
        return !result.Any() ? null : result.Cast<User>();
    }

    public Message? GetMessageById(int id)
    {
        var sql = $"SELECT * FROM table_history WHERE id = {id}";
        return this.FindOne(sql) as Message;
    }

    public IEnumerable<Message>? GetAllMessages()
    {
        var sql = "SELECT * FROM table_history";
        var result = this.FindAll(sql);
        return !result.Any() ? null : result.Cast<Message>();
    }

    private object? FindOne(string sql)
    {
        this.Db.Open();
        var result = this.Db.QuerySingleOrDefault(sql);
        this.Db.Close();

        return result;
    }

    private IEnumerable<object> FindAll(string sql)
    {
        this.Db.Open();
        var result = this.Db.Query(sql);
        this.Db.Close();

        return result;
    }
}
