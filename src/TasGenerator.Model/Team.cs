using System;

namespace TasGenerator.Model
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }
        public string Country { get; set; }
        public Group Group { get; set; }
        public bool IsDone { get; set; }


        public Team()
        {

        }

        public Team(int id, string name, int rank, string country, Group group)
        {
            this.Name = name;
            this.Rank = rank;
            this.Country = country;
            this.Group = group;
            IsDone = false;
        }

        public override string ToString()
        {
            return Name + ", (" + Country + ")";
        }

        public void PrintTeam(int level)
        {
            for (int i = 0; i < level; i++)
                Console.Write(" ");
            Console.WriteLine(string.Format("- {0}", ToString()));
        }

        public override bool Equals(object obj)
        {
            var teamB = obj as Team;
            if (teamB == null) return false;
            return GetHashCode() == teamB.GetHashCode();
        }

        public override int GetHashCode()
        {
            return Id;
        }

        //public async Task Save()
        //{

        //    try
        //    {
        //        var db = DbHelper.Instance.Database;

        //        var collection = db.GetCollection<Team>("teams");
        //        await collection.InsertOneAsync(this);


        //    }
        //    catch (Exception ex)
        //    {
        //    var d = ex;
        //    }
        //}
    }
}

