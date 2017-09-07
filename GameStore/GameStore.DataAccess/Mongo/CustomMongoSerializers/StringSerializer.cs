using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace GameStore.DataAccess.Mongo.CustomMongoSerializers
{
    sealed class StringSerializer : SerializerBase<string>
    {
        public override string Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonType = context.Reader.GetCurrentBsonType();
            switch (bsonType)
            {
                case BsonType.Null:
                    context.Reader.ReadNull();
                    return null;
                case BsonType.Int32:
                    return context.Reader.ReadInt32().ToString();
                default:
                    var message = $"Cannot deserialize BsonInt32 from BsonType {bsonType}.";

                    throw new BsonSerializationException(message);
            }
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, string gameKey)
        {
            var bsonWriter = context.Writer;
            if (gameKey != null)
            {
                bsonWriter.WriteInt32(int.Parse(gameKey));
            }
            else
            {
                bsonWriter.WriteNull();
            }
        }
    }
}
