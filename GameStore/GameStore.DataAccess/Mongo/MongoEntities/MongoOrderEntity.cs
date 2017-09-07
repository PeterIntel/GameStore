using System;
using System.Collections.Generic;
using GameStore.DataAccess.Mongo.CustomMongoSerializers;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.DataAccess.Mongo.MongoEntities
{
    [BsonIgnoreExtraElements]
    public class MongoOrderEntity : BasicMongoEntity
    {
        public string CustomerID { set; get; }

        [BsonSerializer(typeof(StringSerializer))]
        public string OrderID { set; get; }

        [BsonSerializer(typeof(DateTimeCustomSerializer))]
        public DateTime? OrderDate { set; get; }

        [BsonIgnore]
        public IEnumerable<MongoOrderDetailsEntity> OrderDetails { set; get; }
    }
}
