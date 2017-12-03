using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TasGenerator.Helper;
using TasGenerator.Model;

namespace TasGenerator
{

    class DrawHelper
    {
        private static DrawHelper instance;

        private DrawHelper()
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

        const string letters = "ABCDEFGH";

        public static DrawHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DrawHelper();
                }
                return instance;
            }
        }

        public Group[] Groups;

        public List<Checkable<Team>> Rules { get; set; } = new List<Checkable<Team>>();

        public Group ParseGroup(string code)
        {
            return Groups[letters.IndexOf(code, 0)];
        }

        private List<Team> teams = new List<Team>();

        //public List<Team> GetAllTeams()
        //{
        //    if (teams.Count == 0)
        //    {
        //        teams.Add(
        //            new Team(1, "Real", 1, "Spain", ParseGroup("A")));
        //        teams.Add(
        //           new Team(2, "PSG", 2, "France", ParseGroup("A")));
        //        teams.Add(
        //           new Team(3, "Wolfsburg", 1, "Germany", ParseGroup("B")));
        //        teams.Add(
        //           new Team(4, "PSV", 2, "Netherlands", ParseGroup("B")));
        //        teams.Add(
        //           new Team(5, "Atletico", 1, "Spain", ParseGroup("C")));
        //        teams.Add(
        //           new Team(6, "Benfica", 2, "Portugal", ParseGroup("C")));
        //        teams.Add(
        //           new Team(7, "Man City", 1, "England", ParseGroup("D")));
        //        teams.Add(
        //           new Team(8, "Juventus", 2, "Italy", ParseGroup("D")));
        //        teams.Add(
        //           new Team(9, "Barcelone", 1, "Spain", ParseGroup("E")));
        //        teams.Add(
        //           new Team(10, "Roma", 2, "Italy", ParseGroup("E")));
        //        teams.Add(
        //           new Team(11, "Bayern", 1, "Germany", ParseGroup("F")));
        //        teams.Add(
        //           new Team(12, "Arsenal", 2, "England", ParseGroup("F")));
        //        teams.Add(
        //           new Team(13, "Chelsea", 1, "England", ParseGroup("G")));
        //        teams.Add(
        //           new Team(14, "Dynamo kiev", 2, "Ukraine", ParseGroup("G")));
        //        teams.Add(
        //           new Team(15, "Zenith", 1, "Russia", ParseGroup("H")));
        //        teams.Add(
        //           new Team(16, "Gent", 2, "Belgium", ParseGroup("H")));
        //    }
        //    return teams;
        //}

        public List<Team> GetAllTeams()
        {
            if (teams.Count == 0)
            {
                teams.Add(
                    new Team(1, "Man Utd", 1, "England", ParseGroup("A")));
                teams.Add(
                   new Team(2, "FC Bale", 2, "Switzerland", ParseGroup("A")));
                //teams.Add(
                //   new Team(3,"CSKA Moskva", 2, "Russia",ParseGroup( "A"));
                teams.Add(
                   new Team(4, "PSG", 1, "France", ParseGroup("B")));
                teams.Add(
                   new Team(5, "Bayern", 2, "Germany", ParseGroup("B")));
                teams.Add(
                   new Team(6, "Chelsea FC", 1, "England", ParseGroup("C")));
                teams.Add(
                   new Team(7, "As Roma", 2, "Italy", ParseGroup("C")));
                //teams.Add(
                //    new Team(8,"Atletico de Madrid", 2, "Spain", "C")));
                teams.Add(
                   new Team(9, "FC Barcelona", 1, "Spain", ParseGroup("D")));
                teams.Add(
                   new Team(10, "Juventus", 2, "Italy", ParseGroup("D")));
                //             teams.Add(
                //new Team(11,"Sporting Portugal", 2, "Portugal",ParseGroup( "D")));

                teams.Add(
                   new Team(12, "Liverpool FC", 1, "England", ParseGroup("E")));
                teams.Add(
                   new Team(13, "Sevilla FC", 2, "Spain", ParseGroup("E")));
                //teams.Add(
                //new Team(14,"FC Spartak Moskva", 2, "Russia", ParseGroup("E")));
                teams.Add(
                   new Team(15, "Man City", 1, "England", ParseGroup("F")));
                teams.Add(
                   new Team(16, "Shaktar Donetsk", 2, "Ukraine", ParseGroup("F")));
                teams.Add(
                   new Team(17, "Besiktas", 1, "Turkey", ParseGroup("G")));
                teams.Add(
                   new Team(18, "FC Porto", 2, "Portugal", ParseGroup("G")));
                //  teams.Add(
                //new Team(19,"Leipzig", 2, "Germany",ParseGroup( "G")));
                teams.Add(
                   new Team(20, "Tottenham", 1, "England", ParseGroup("H")));
                teams.Add(
                   new Team(21, "Real Madrid", 2, "Spain", ParseGroup("H")));
            }
            return teams;
        }

        public List<DrawGroup> GetDrawGroupsByGroup()
        {
            return GetAllTeams().GroupBy(t => t.Group).Select(g => new DrawGroup()
            { GroupIdentifier = g.Key.Name, Teams = g.ToList() }).ToList();
        }


        public List<DrawGroup> GetDrawGroupsByRank()
        {
            return GetAllTeams().GroupBy(t => t.Rank).Select(g => new DrawGroup()
            { GroupIdentifier = g.Key.ToString(), Teams = g.ToList() }).ToList();
        }


        public bool RemoveAndCheck(Dictionary<Team, List<Team>> team, Team current, Team opponentInAnotherMatch)
        {
            if (team[current].Contains(opponentInAnotherMatch))
            {
                team[current].Remove(opponentInAnotherMatch);
            }
            return team[current].Count > 0;
        }

        public enum TASStatus
        {
            Continue,
            Stop,
            DeadEnd
        }

        private Dictionary<Team, List<Team>> CopyDictionary(Dictionary<Team, List<Team>> old)
        {
            var newDico = new Dictionary<Team, List<Team>>();
            foreach (var kp in old)
            {
                newDico.Add(kp.Key, kp.Value.ToList());
            }
            return newDico;
        }


    }
}
