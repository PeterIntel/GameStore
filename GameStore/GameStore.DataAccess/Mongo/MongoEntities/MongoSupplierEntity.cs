using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.DataAccess.Mongo.MongoEntities
{
    [BsonIgnoreExtraElements]
    public class MongoSupplierEntity : BasicMongoEntity
    {
        public int SupplierID { set; get; }

        public string CompanyName { set; get; }

        public string HomePage { set; get; }
        [BsonIgnore]
        public IEnumerable<MongoProductEntity> Products { set; get; }
    }
}
