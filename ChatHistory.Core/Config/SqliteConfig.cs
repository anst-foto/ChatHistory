using System.Text.Json;

namespace ChatHistory.Core.Config;

public sealed class SqliteConfig : IDbConfig, IEquatable<SqliteConfig>
{
    public string? Path { get; set; }

    public string ToConnectionString()
    {
        if (this.Path is null)
        {
            throw new ConfigException($"{nameof(this.Path)} is null");
        }

        return $"Data Source={this.Path};";
    }

    public override string ToString() => this.ToConnectionString();

    public static SqliteConfig? GetFromJson(string path = "db_config.json")
    {
        try
        {
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<SqliteConfig>(json);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public bool Equals(SqliteConfig? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return this.Path == other.Path;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != this.GetType())
        {
            return false;
        }

        return this.Equals((SqliteConfig)obj);
    }

    public override int GetHashCode() => this.Path?.GetHashCode() ?? 0;

    public static bool operator ==(SqliteConfig? left, SqliteConfig? right) => Equals(left, right);

    public static bool operator !=(SqliteConfig? left, SqliteConfig? right) => !Equals(left, right);
}
