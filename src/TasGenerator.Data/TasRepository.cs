using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Collections.Generic;
using TasGenerator.Model;

namespace TasGenerator.Data
{
    public class TasRepository : ITasRepository
    {
        MongoClient _client;
        IMongoDatabase _db;

        public TasRepository()
        {
            var connectionString = "mongodb://tas1:tas11@ds035014.mongolab.com:35014/caveandi";

            _client = new MongoClient(connectionString);
            _db = _client.GetDatabase("caveandi");

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

        public IEnumerable<Team> GetTeams()
        {
            var filter = new BsonDocument();
            return _db.GetCollection<Team>("teams").Find(filter).ToList();
        }


        public Team GetTeam(int id)
        {
            var filter = Builders<Team>.Filter.Eq("id", id);
            return _db.GetCollection<Team>("teams").Find(filter).First();
        }

        public Team Create(Team p)
        {
            _db.GetCollection<Team>("teams").InsertOne(p);
            return p;
        }

        public void Update(int id, Team p)
        {
            p.Id = id;
            var filter = Builders<Team>.Filter.Eq("id", p.Id);
            _db.GetCollection<Team>("teams").FindOneAndReplace(filter, p);
        }
        public void Remove(int id)
        {
            var filter = Builders<Team>.Filter.Eq("id",id);
            _db.GetCollection<Team>("teams").DeleteOne(filter);
        }

        //public IEnumerable<Competition> GetCompetitions()
        //{
        //    return _db.GetCollection<Competition>("competition").FindAll();
        //}


        //public Competition GetCompetition(string name)
        //{
        //    var res = Query<Competition>.EQ(p => p.Name, name);
        //    return _db.GetCollection<Competition>("competition").FindOne(res);
        //}

        //public Competition CreateCompetition(Competition p)
        //{
        //    _db.GetCollection<Competition>("competition").Save(p);
        //    return p;
        //}

        //public void UpdateCompetition(string name, Competition p)
        //{
        //    p.Name = name;
        //    var res = Query<Competition>.EQ(pd => pd.Name, name);
        //    var operation = Update<Competition>.Replace(p);
        //    _db.GetCollection<Competition>("competition").Update(res, operation);
        //}
        //public void RemoveCompetition(string name)
        //{
        //    var res = Query<Competition>.EQ(e => e.Name, name);
        //    var operation = _db.GetCollection<Competition>("competition").Remove(res);
        //}




        //public IEnumerable<DrawSolution> GetDrawSolutions()
        //{
        //    return _db.GetCollection<DrawSolution>("drawsolution").FindAll();
        //}

        //public DrawSolution GetDrawSolution(ObjectId id)
        //{
        //    var res = Query<DrawSolution>.EQ(p => p.Id, id);
        //    return _db.GetCollection<DrawSolution>("drawsolution").FindOne(res);
        //}

        //public DrawSolution CreateDrawSolution(DrawSolution p)
        //{
        //    _db.GetCollection<DrawSolution>("drawsolution").Save(p);
        //    return p;
        //}

        //public void UpdateDrawSolution(ObjectId id, DrawSolution p)
        //{
        //    p.Id = id;
        //    var res = Query<DrawSolution>.EQ(pd => pd.Id, id);
        //    var operation = Update<DrawSolution>.Replace(p);
        //    _db.GetCollection<DrawSolution>("drawsolution").Update(res, operation);
        //}
        //public void RemoveDrawSolution(ObjectId id)
        //{
        //    var res = Query<DrawSolution>.EQ(e => e.Id, id);
        //    var operation = _db.GetCollection<DrawSolution>("drawsolution").Remove(res);
        //}
    }
}