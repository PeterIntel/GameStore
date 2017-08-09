using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataAccess.Mongo.MongoEntities
{
    [BsonIgnoreExtraElements]
    public class MongoOrderDetailsEntity : BasicMongoEntity
    {
        public int ProductID { set; get; }
        public int OrderID { set; get; }
        public decimal Price { set; get; }
        public short Quantity { set; get; }
        public double Discount { set; get; }
        [BsonIgnore]
        public MongoProductEntity Product { set; get; }
        [BsonIgnore]
        public MongoOrderEntity Order { set; get; }
    }
}
