
using System;
using System.Globalization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace GameStore.DataAccess.Mongo.CustomMongoSerializers
{
    public class DateTimeCustomSerializer : SerializerBase<DateTime?>
    {
        public override DateTime? Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonReader = context.Reader;
            var bsonType = bsonReader.CurrentBsonType;
            switch (bsonType)
            {
                case BsonType.Null:
                    bsonReader.ReadNull();
                    return null;
                case BsonType.String:
                    {
                        var dateString = bsonReader.ReadString();
                        var output = ParseDateTime(dateString);
                        if (output == null)
                        {
                            throw new BsonSerializationException($"Cannot deserialize BsonDateTime from BsonType {bsonType}.");
                        }
                        return output;
                    }

                default:
                    throw new BsonSerializationException($"Cannot deserialize BsonDateTime from BsonType {bsonType}.");
            }
        }

        private static DateTime? ParseDateTime(string dateString)
        {
            DateTime output;
            var parsedSuccesfully = DateTime.TryParseExact(dateString,
                "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out output);

            if (parsedSuccesfully)
            {
                return output;
            }
            return null;
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateTime? nullableDateTime)
        {
            var bsonWriter = context.Writer;
            if (nullableDateTime != null)
            {
                bsonWriter.WriteString(nullableDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            }
            else
            {
                bsonWriter.WriteNull();
            }
        }
    }
}

