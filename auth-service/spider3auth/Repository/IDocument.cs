using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonUtilities.Repository
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        ObjectId Id { get; set; }

        DateTime createdAt { get; }
    }
}
