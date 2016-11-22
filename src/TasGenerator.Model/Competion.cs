using System.Collections.Generic;

namespace TasGenerator.Model
{
    public  class Competition
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Season Season { get; set; }

        public List<DrawInfo> Draws { get; set; } = new List<DrawInfo>();

        //public async Task  Save()
        //{

        //    try
        //    {
        //        List<Task> taskList = new List<Task>();
        //        foreach (var draw in Draws)
        //            taskList.Add(draw.DrawDetail.SaveSolutions());

        //        Task.WaitAll(taskList.ToArray());

        //        var db = DbHelper.Instance.Database;

        //        var collection = db.GetCollection<Competition>("competition");
        //        await collection.InsertOneAsync(this);


        //    }
        //    catch (Exception ex)
        //    {
        //        var d = ex;
        //    }
        //}
    }
}
