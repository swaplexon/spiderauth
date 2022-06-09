using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace spider3auth.Repository
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        ObjectId Id { get; set; }

        DateTime createdAt { get; }
    }
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime createdAt => Id.CreationTime;
    }
}
