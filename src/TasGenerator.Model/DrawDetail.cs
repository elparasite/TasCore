using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TasGenerator.Model
{
    public class DrawDetail
    {
        [BsonIgnore]
        public List<DrawSolution> MatchSolution { get; set; } = new List<DrawSolution>();

       public ObjectId[] SolutionsId { get; set; }

        //public async Task SaveSolutions()
        //{

        //    try
        //    {
        //        var db = DbHelper.Instance.Database;

        //        var collection = db.GetCollection<DrawSolution>("drawsolution");
        //        await collection.InsertManyAsync(MatchSolution);
        //        SolutionsId = MatchSolution.Select(_ => _.Id).ToArray();

        //    }
        //    catch (Exception ex)
        //    {
        //        var d = ex;
        //    }
        //}
    }
}
