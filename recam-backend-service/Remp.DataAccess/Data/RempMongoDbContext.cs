using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Remp.DataAccess.Collections;

namespace Remp.DataAccess.Data;

public class RempMongoDbContext
{
    private readonly IMongoDatabase _database;

    public RempMongoDbContext(IOptions<RempMongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
        InitializeIndexes();
    }

    public IMongoCollection<CaseHistoryEvent> CaseHistoryEvents => _database.GetCollection<CaseHistoryEvent>("caseHistoryEvents");
    public IMongoCollection<UserActivityLogEvent> UserActivityLogEvents => _database.GetCollection<UserActivityLogEvent>("userActivityLogEvents");

    private void InitializeIndexes()
    {
        var caseHistoryListingCaseIndexKeys = Builders<CaseHistoryEvent>.IndexKeys
            .Ascending(e => e.ListingCaseId)
            .Ascending(e => e.ChangedByUserId)
            .Descending(e => e.ChangedAt);
        CaseHistoryEvents.Indexes.CreateOne(new CreateIndexModel<CaseHistoryEvent>(caseHistoryListingCaseIndexKeys));
        // Other composite indexes are commented out for now; re-enable them when filtering by user or resource is needed.
        // var userActivityLogUserIdIndexKeys = Builders<UserActivityLogEvent>.IndexKeys
        //     .Ascending(e => e.UserId)
        //     .Ascending(e => e.ResourceId)
        //     .Descending(e => e.OperationTime);
        // UserActivityLogEvents.Indexes.CreateOne(new CreateIndexModel<UserActivityLogEvent>(userActivityLogUserIdIndexKeys));
        // var userActivityLogResourceIdIndexKeys = Builders<UserActivityLogEvent>.IndexKeys
        //     .Ascending(e => e.ResourceId)
        //     .Ascending(e => e.UserId)
        //     .Descending(e => e.OperationTime);
        // UserActivityLogEvents.Indexes.CreateOne(new CreateIndexModel<UserActivityLogEvent>(userActivityLogResourceIdIndexKeys));
        var userActivityLogOperationTimeIndexKeys = Builders<UserActivityLogEvent>.IndexKeys
            .Descending(e => e.OperationTime);
        UserActivityLogEvents.Indexes.CreateOne(new CreateIndexModel<UserActivityLogEvent>(userActivityLogOperationTimeIndexKeys));
    }
}
