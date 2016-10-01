using MongoDB.Bson.Serialization.Attributes;
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
        [BsonIgnore]
        public DrawGroup DrawGroup { get; set; }

        public Team()
        {

        }

        public Team(int id,string name, int rank, string country, string group)
        {
            this.Name = name;
            this.Rank = rank;
            this.Country = country;
            this.Group = Group.Groups[letters.IndexOf(group, 0)];
            IsDone = false;
        }

        //public bool CanPlayVs(Checkable<Team> rules,Team opponent)
        //{
        //    if (opponent == null) return false;
        //    if ()
        //}

        const string letters = "ABCDEFGH";

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

    public class Group
    {
        public string Name { get; set; }

        public static Group[] Groups;

        static Group()
        {
            Groups = new Group[8];
            Groups[0] = new Group() { Name = "A" };
            Groups[1] = new Group() { Name = "B" };
            Groups[2] = new Group() { Name = "C" };
            Groups[3] = new Group() { Name = "D" };
            Groups[5] = new Group() { Name = "E" };
            Groups[4] = new Group() { Name = "F" };
            Groups[6] = new Group() { Name = "G" };
            Groups[7] = new Group() { Name = "H" };
        }

        public override bool Equals(object obj)
        {

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}

