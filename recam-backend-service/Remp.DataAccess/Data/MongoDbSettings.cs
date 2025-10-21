using System;

namespace Remp.DataAccess.Data;

public class RempMongoDbSettings
{
    public string ConnectionString { get; set; } = string.Empty;

    public string DatabaseName { get; set; } = string.Empty;
}
