using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasGenerator.Model
{
    public class DrawSolution
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public int NumberItems { get; set; }
        public List<DrawItem> DrawItems { get; set; } = new List<DrawItem>();

        public int UniqueCode { get; set; }

        public DrawSolution()
        {

        }
        public DrawSolution(int numberItems)
        {
            DrawItems = new List<DrawItem>();
            NumberItems = numberItems;
            UniqueCode = 0;
        }

        public DrawSolution(DrawSolution currentSolution)
        {
            this.UniqueCode = currentSolution.UniqueCode;
            this.DrawItems = new List<DrawItem>(currentSolution.DrawItems);
        }

        public bool AddItem(DrawItem match)
        {
            if (DrawItems == null)
                DrawItems = new List<DrawItem>();
            else if (DrawItems.Contains(match)) return false;
            DrawItems.Add(match);
            UniqueCode += match.GetHashCode();
            return true;
        }

        internal void RemoveItem(DrawItem currentMatch)
        {
            DrawItems.Remove(currentMatch);
            UniqueCode -= currentMatch.GetHashCode();
        }

        public void PrintSolution(bool inFile = false)
        {
           // DrawItems.Sort();
            if (inFile)
                File.AppendText("=============================================================");
            else
                Console.WriteLine("=============================================================");
            foreach (var match in DrawItems)
            {
                if (inFile)
                    File.AppendText(match.ToString());
                else
                    Console.WriteLine(match.ToString());
            }
        }

        public void Sort()
        {
            DrawItems.Sort();
        }

        public override bool Equals(object obj)
        {
            var solution = obj as DrawSolution;
            if (solution == null) return false;
            return solution.GetHashCode() == GetHashCode();
        }

        public void RefreshCode()
        {
            var newCode = DrawItems.Sum(m => m.GetHashCode());
            if (UniqueCode != newCode)
                UniqueCode = newCode;
        }

        public override int GetHashCode()
        {
            return UniqueCode;
        }
        public static int NumberOfDrawItemInCommon(List<DrawSolution> allSolutions, string firstRankTEam, string secondRankTeam)
        {
            int count = 0;
            foreach (var sol in allSolutions)
            {
                foreach (var match in sol.DrawItems)
                {
                    if (match.Teams.Any(t => t.Name == secondRankTeam) && match.Teams.Any(t => t.Name == firstRankTEam))
                        count++;
                }
            }
            return count;
        }


    }
}
