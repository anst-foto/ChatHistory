using ChatHistory.Core.Config;

namespace ChatHistory.Core.Test;

public class SqliteContextTest
{
    private readonly SqliteConfig _sqliteConfig = new()
    {
        Path = "history.db"
    };
}