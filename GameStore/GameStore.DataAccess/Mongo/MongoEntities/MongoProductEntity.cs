using System.Collections.Generic;
using GameStore.DataAccess.Mongo.CustomMongoSerializers;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.DataAccess.Mongo.MongoEntities
{
    [BsonIgnoreExtraElements]
    public class MongoProductEntity : BasicMongoEntity
    {
        [BsonSerializer(typeof(StringSerializer))]
        public string ProductID { set; get; }

        public string ProductName { set; get; }

        public int SupplierID { set; get; }

        public int CategoryID { set; get; }

        public double UnitPrice { set; get; }

        public int UnitsInStock { set; get; }

        public bool Discontinued { set; get; }

        [BsonIgnore]
        public IEnumerable<MongoCategoryEntity> Categories { set; get; }

        [BsonIgnore]
        public MongoSupplierEntity Supplier { set; get; }
    }
}
