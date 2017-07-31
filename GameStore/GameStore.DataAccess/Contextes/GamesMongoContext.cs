using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Configuration;

namespace GameStore.DataAccess.Contextes
{
    public class GamesMongoContext
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        public GamesMongoContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["GameStoreMongoContext"].ConnectionString;
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase()
        }
    }
}
