using System.Globalization;
using ChatHistory.Core.Config;
using ChatHistory.Core.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace ChatHistory.Core.Context;

public class SqliteContext : DbContext, IDbContext
{
    public SqliteContext(string path = "db_config.json") : base(SqliteConfig.GetFromJson(path), new SqliteConnection())
    {
        var connectionString = this.Config?.ToString();
        this.Db.ConnectionString = connectionString;
    }

    public User? GetUserById(int id)
    {
        var parameters = new { Id = id };
        const string sql = "SELECT * FROM table_users WHERE id = @Id";
        this.Db.Open();
        var result = this.Db.QuerySingleOrDefault<User>(sql, parameters);
        this.Db.Close();

        return result;
    }

    public IEnumerable<User>? GetUsersByName(string name)
    {
        var parameters = new { Name = name };
        const string sql = "SELECT * FROM table_users WHERE name = @Name";
        this.Db.Open();
        var result = this.Db.Query<User>(sql, parameters);
        this.Db.Close();

        return result.Any() ? result : null;
    }

    public IEnumerable<User>? GetAllUsers()
    {
        const string sql = "SELECT * FROM table_users";
        this.Db.Open();
        var result = this.Db.Query<User>(sql);
        this.Db.Close();

        return result.Any() ? result : null;
    }

    public Message? GetMessageById(int id)
    {
        var parameters = new { Id = id };
        const string sql = $"SELECT * FROM table_history WHERE id = @Id";
        this.Db.Open();
        var result = this.Db.QuerySingleOrDefault(sql, parameters);
        this.Db.Close();

        if (result is null)
        {
            return null;
        }

        var sender = this.GetUserById((int)result.sender_id);
        var receiver = this.GetUserById((int)result.receiver_id);
        var replyMessage = (int?)result.reply_message_id is null
            ? null
            : this.GetMessageById((int)result.reply_message_id);
        var dateSend = DateTime.Parse((string)result.date_send, CultureInfo.InvariantCulture);
        var isReceive = (int)result.is_receive != 0;
        var isRead = (int)result.is_read != 0;
        var isDelete = (int)result.is_delete != 0;

        return new Message()
        {
            Id = (int)result.id,
            Sender = sender,
            Receiver = receiver,
            MessageText = (string)result.message,
            DateSend = dateSend,
            ReplyMessage = replyMessage,
            IsReceive = isReceive,
            IsRead = isRead,
            IsDelete = isDelete
        };
    }

    public IEnumerable<Message?>? GetAllMessages()
    {
        const string sql = "SELECT id FROM table_history";
        this.Db.Open();
        var result = this.Db.Query(sql);
        this.Db.Close();

        foreach (var item in result)
        {
            var id = (int)item.id;
            yield return this.GetMessageById(id);
        }
    }
}
