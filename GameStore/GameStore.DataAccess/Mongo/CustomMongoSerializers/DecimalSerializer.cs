using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataAccess.Mongo.CustomMongoSerializers
{
    sealed class DecimalSerializer : SerializerBase<decimal>
    {
        public override decimal Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonType = context.Reader.GetCurrentBsonType();
            switch (bsonType)
            {
                case BsonType.Double:
                    return (decimal)context.Reader.ReadDouble();
                default:
                    var message = $"Cannot deserialize BsonInt32 from BsonType {bsonType}.";
                    throw new BsonSerializationException(message);
            }
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, decimal price)
        {
            var bsonWriter = context.Writer;
            if (price != null)
            {
                bsonWriter.WriteDouble((double)price);
            }
            else
            {
                bsonWriter.WriteNull();
            }
        }
    }
}
