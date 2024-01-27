using System.Text.Json;

namespace ChatHistory.Core.Config;

public class SqliteConfig : IDbConfig
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
}