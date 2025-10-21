using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Remp.DataAccess.Data;

public class RempMongoDbContext
{
    private readonly IMongoDatabase _database;

    public RempMongoDbContext(IOptions<RempMongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }
    // TODO: add collections
}
