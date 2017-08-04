using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.DataAccess.MongoEntities
{
    public class MongoProduct
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { set; get; }
        public string ProductName { set; get; }
        public int SupplierID { set; get; }
        public int CategoryID { set; get; }
        public int UnitPrice { set; get; }
        public short UnitsInStock { set; get; }
        public bool Discontinued { set; get; }
        [BsonIgnore]
        public IEnumerable<MongoCategory> Categories { set; get; }
        [BsonIgnore]
        public MongoSupplier Supplier { set; get; }
    }
}
