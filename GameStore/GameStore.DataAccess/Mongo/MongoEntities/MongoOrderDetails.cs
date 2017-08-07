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
    public class MongoOrderDetails
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { set; get; }
        public int ProductID { set; get; }
        public int OrderID { set; get; }
        public decimal Price { set; get; }
        public short Quantity { set; get; }
        public double Discount { set; get; }
        [BsonIgnore]
        public MongoProduct Product { set; get; }
        [BsonIgnore]
        public MongoOrder Order { set; get; }
    }
}
