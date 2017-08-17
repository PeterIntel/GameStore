using System.Configuration;
using MongoDB.Driver;

namespace GameStore.DataAccess.Mongo
{
    public class GamesMongoContext
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        public GamesMongoContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["GameStoreMongoContext"].ConnectionString;
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(MongoUrl.Create(connectionString).DatabaseName);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return _database.GetCollection<TEntity>(CollectionName.CollectionNames[typeof(TEntity)]);
        }
    }
}
