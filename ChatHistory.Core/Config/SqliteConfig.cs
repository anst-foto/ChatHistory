using System.Text.Json;

namespace ChatHistory.Core.Config;

public class SqliteConfig : IDbConfig, IEquatable<SqliteConfig>
{
    public string Path { get; set; }

    public string ToConnectionString() => $"Data Source={Path};";

    public override string ToString()
    {
        return ToConnectionString();
    }

    public static SqliteConfig? GetFromJson(string path = "db_config.json")
    {
        try
        {
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<SqliteConfig>(json);
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public bool Equals(SqliteConfig? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Path == other.Path;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((SqliteConfig)obj);
    }

    public override int GetHashCode()
    {
        return Path.GetHashCode();
    }

    public static bool operator ==(SqliteConfig? left, SqliteConfig? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(SqliteConfig? left, SqliteConfig? right)
    {
        return !Equals(left, right);
    }
}