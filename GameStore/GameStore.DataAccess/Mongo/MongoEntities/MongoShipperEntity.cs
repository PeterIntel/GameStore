using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

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
