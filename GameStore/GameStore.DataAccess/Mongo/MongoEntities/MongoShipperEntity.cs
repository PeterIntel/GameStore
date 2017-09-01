using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.DataAccess.Mongo.MongoEntities
{
    [BsonIgnoreExtraElements]
    public class MongoShipperEntity : BasicMongoEntity
    {
        public int ShipperID { set; get; }
        public string CompanyName { set; get; }
        public string Phone { set; get; }
    }
}
