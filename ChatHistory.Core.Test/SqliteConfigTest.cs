using ChatHistory.Core.Config;

namespace ChatHistory.Core.Test;

public class SqliteConfigTest
{
    private readonly SqliteConfig _sqliteConfig = new() { Path = "history.db" };

    [Fact]
    public void GetFromJsonTestPositive()
    {
        var expected = this._sqliteConfig;
        var actual = SqliteConfig.GetFromJson();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetFromJsonTestNegative()
    {
        var actual = SqliteConfig.GetFromJson("json_config_bad.json");
        Assert.Null(actual);
    }

    [Fact]
    public void ToConnectionStringTest()
    {
        var expected = this._sqliteConfig.ToConnectionString();
        var actual = SqliteConfig.GetFromJson()!.ToConnectionString();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToStringTest()
    {
        var expected = this._sqliteConfig.ToString();
        var actual = SqliteConfig.GetFromJson()!.ToString();
        Assert.Equal(expected, actual);
    }
}
