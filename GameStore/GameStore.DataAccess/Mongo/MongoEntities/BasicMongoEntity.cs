using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.DataAccess.Mongo.MongoEntities
{
    public class BasicMongoEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { set; get; }
    }
}
