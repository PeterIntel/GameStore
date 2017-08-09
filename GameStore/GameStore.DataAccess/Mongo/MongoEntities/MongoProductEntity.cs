using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.DataAccess.Mongo.MongoEntities
{
    [BsonIgnoreExtraElements]
    public class MongoProductEntity : BasicMongoEntity
    {
        public int ProductID { set; get; }
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
