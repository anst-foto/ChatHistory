using System.Data;
using ChatHistory.Core.Config;

namespace ChatHistory.Core.Context;

public abstract class DbContext
{
    protected readonly IDbConfig Config;
    protected readonly IDbConnection Db;

    protected DbContext(IDbConfig config, IDbConnection db)
    {
        Config = config;
        Db = db;
    }
}