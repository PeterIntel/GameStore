using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace GameStore.DataAccess.MongoEntities
{
    public class MongoShipper
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { set; get; }
        public int ShipperID { set; get; }
        public string CompanyName { set; get; }
        public string Phone { set; get; }
    }
}
