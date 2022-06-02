using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using spider3auth.Repository;

namespace spider3auth.Entities
{
    [BsonCollection("groupitem")]
    public class GroupItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Description { get; set; }
    }
}
