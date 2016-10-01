using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasGenerator.Model;

namespace TasGenerator.Helper
{
    class DbHelper
    {
        private static DbHelper instance;

        private DbHelper() {
            var connectionString = "mongodb://tas1:tas11@ds035014.mongolab.com:35014/caveandi";

            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase("caveandi");

            BsonClassMap.RegisterClassMap<Group>();
            BsonClassMap.RegisterClassMap<Team>();
            BsonClassMap.RegisterClassMap<Season>();
            BsonClassMap.RegisterClassMap<SpecialExclusion>();
            BsonClassMap.RegisterClassMap<DrawItem>();
            BsonClassMap.RegisterClassMap<DrawSolution>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            }); ;
            BsonClassMap.RegisterClassMap<DrawDetail>();
            BsonClassMap.RegisterClassMap<DrawInfo>();
            BsonClassMap.RegisterClassMap<Competition>();
        }

        public static DbHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DbHelper();
                }
                return instance;
            }
        }

        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        public IMongoDatabase Database { get { return _database; } }


    }
}
