using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Remp.Model.Enums;

namespace Remp.DataAccess.Collections;

public class UserActivityLogEvent
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; init; } = string.Empty;

    [BsonElement("userId")]
    public string UserId { get; init; } = string.Empty;
    [BsonElement("resource")]
    [BsonRepresentation(BsonType.String)]
    public UserActivityResourceType ResourceType { get; init; }
    [BsonElement("resourceId")]
    public string ResourceId { get; init; } = string.Empty;
    [BsonElement("action")]
    [BsonRepresentation(BsonType.String)]
    public UserActivityActionType Activity { get; init; }
    [BsonElement("isSuccessful")]
    public bool IsSuccessful { get; init; }
    [BsonElement("eventDetails")]
    [BsonIgnoreIfNull]
    public BsonDocument? Metadata { get; init; }
    [BsonElement("operationTime")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime OperationTime { get; init; } = DateTime.UtcNow;

}
