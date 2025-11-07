using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Remp.Model.Enums;


namespace Remp.DataAccess.Collections;

public class CaseHistoryEvent
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; init; } = string.Empty;

    [BsonElement("changedByUserId")]
    public string ChangedByUserId { get; init; } = string.Empty;
    [BsonElement("listingCaseId")]
    public int ListingCaseId { get; init; }
    [BsonElement("changeType")]
    [BsonRepresentation(BsonType.String)]
    public ListingCaseChangeType ChangeType { get; init; }
    [BsonElement("changedDetails")]
    [BsonIgnoreIfNull]
    public IReadOnlyList<FieldChange>? ChangedDetails { get; init; }
    [BsonElement("changedAt")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime ChangedAt { get; init; } = DateTime.UtcNow;
    public class FieldChange
    {
        [BsonElement("field")]
        public string Field { get; init; } = string.Empty;
        [BsonElement("oldValue")]
        public string? OldValue { get; init; }
        [BsonElement("newValue")]
        public string? NewValue { get; init; }
    }
}

