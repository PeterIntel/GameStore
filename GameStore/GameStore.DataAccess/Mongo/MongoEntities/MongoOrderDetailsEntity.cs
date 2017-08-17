using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Mongo.CustomMongoSerializers;

namespace GameStore.DataAccess.Mongo.MongoEntities
{
    [BsonIgnoreExtraElements]
    public class MongoOrderDetailsEntity : BasicMongoEntity
    {
        [BsonSerializer(typeof(StringSerializer))]
        public string ProductID { set; get; }
        [BsonSerializer(typeof(StringSerializer))]
        public string OrderID { set; get; }
        [BsonSerializer(typeof(DecimalSerializer))]
        public decimal UnitPrice { set; get; }
        public short Quantity { set; get; }
        public double Discount { set; get; }
        [BsonIgnore]
        public MongoProductEntity Product { set; get; }
        [BsonIgnore]
        public MongoOrderEntity Order { set; get; }
    }
}
