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

            Countries = new List<Country>()
            {
                 new Country() { Code="SPA",Name="Spain" },
                 new Country() { Code="FRA",Name="France" },
                 new Country() { Code="ENG",Name="England" },
                 new Country() { Code="GER",Name="Germany" },
                 new Country() { Code="ITA",Name="Italy" },
                 new Country() { Code="UKR",Name="Ukraine" },
                 new Country() { Code="POR",Name="Portugal" },
                 new Country() { Code="CRO",Name="Croatia" },
                 new Country() { Code="BEL",Name="Belgium" },
                 new Country() { Code="RUS",Name="Russia" },
                 new Country() { Code="NET",Name="Netherlands" },
                 new Country() { Code="BUL",Name="Bulgaria" },
                 new Country() { Code="DAN",Name="Danemark" },
                 new Country() { Code="POL",Name="Poland" },
                 new Country() { Code="TUR",Name="Turkey" },
                 new Country() { Code="SCO",Name="Scotland" },
                 new Country() { Code="SWI",Name="Switzerland" }
            };

        }

        public static Country Spain { get { return Instance.Countries.Single(c => c.Code == "SPA"); } }
        public static Country France { get { return Instance.Countries.Single(c => c.Code == "FRA"); } }
        public static Country England { get { return Instance.Countries.Single(c => c.Code == "ENG"); } }
        public static Country Germany { get { return Instance.Countries.Single(c => c.Code == "GER"); } }
        public static Country Italy { get { return Instance.Countries.Single(c => c.Code == "ITA"); } }
        public static Country Ukraine { get { return Instance.Countries.Single(c => c.Code == "UKR"); } }
        public static Country Portugal { get { return Instance.Countries.Single(c => c.Code == "POR"); } }
        public static Country Croatia { get { return Instance.Countries.Single(c => c.Code == "CRO"); } }
        public static Country Belgium { get { return Instance.Countries.Single(c => c.Code == "BEL"); } }
        public static Country Russia { get { return Instance.Countries.Single(c => c.Code == "RUS"); } }
        public static Country Netherlands { get { return Instance.Countries.Single(c => c.Code == "NET"); } }
        public static Country Bulgaria { get { return Instance.Countries.Single(c => c.Code == "BUL"); } }
        public static Country Danemark { get { return Instance.Countries.Single(c => c.Code == "DAN"); } }
        public static Country Poland { get { return Instance.Countries.Single(c => c.Code == "POL"); } }
        public static Country Turkey { get { return Instance.Countries.Single(c => c.Code == "TUR"); } }
        public static Country Scotland { get { return Instance.Countries.Single(c => c.Code == "SCO"); } }
        public static Country Switzerland { get { return Instance.Countries.Single(c => c.Code == "SWI"); } }


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

        public Group[] Groups { get; set; }
        public List<Country> Countries { get; set; }

        public List<Checkable<Team>> Rules { get; set; } = new List<Checkable<Team>>();

        public Group ParseGroup(string code)
        {
            return Groups[letters.IndexOf(code, 0)];
        }

        private List<Team> teams = new List<Team>();


        public List<Team> GetAllTeams()
        {
            if (teams.Count == 0)
            {
                teams.Add(
                    new Team(1, "Arsenal", 1, England, ParseGroup("A")));
                teams.Add(
                   new Team(2, "PSG", 2, France, ParseGroup("A")));
                teams.Add(
                   new Team(3, "Naples", 1, Italy, ParseGroup("B")));
                teams.Add(
                   new Team(4, "Benfica", 2, Portugal, ParseGroup("B")));
                teams.Add(
                   new Team(5, "Barcelone", 1, Spain, ParseGroup("C")));
                teams.Add(
                   new Team(6, "Manchester City", 2, England, ParseGroup("C")));
                teams.Add(
                   new Team(7, "Atletico Madrid", 1, Spain, ParseGroup("D")));
                teams.Add(
                   new Team(8, "Bayern", 2, Germany, ParseGroup("D")));
                teams.Add(
                   new Team(9, "Monaco", 1, France, ParseGroup("E")));
                teams.Add(
                   new Team(10, "Bayer Leverkusen", 2, Germany, ParseGroup("E")));
                teams.Add(
                   new Team(11, "Dortmund", 1, Germany, ParseGroup("F")));
                teams.Add(
                   new Team(12, "Real Madrid", 2, Spain, ParseGroup("F")));
                teams.Add(
                   new Team(13, "Leicester", 1, England, ParseGroup("G")));
                teams.Add(
                   new Team(14, "FC Porto", 2, Portugal, ParseGroup("G")));
                teams.Add(
                   new Team(15, "Juventus", 1, Italy, ParseGroup("H")));
                teams.Add(
                   new Team(16, "Seville", 2, Spain, ParseGroup("H")));
            }
            return teams;
        }

        public List<Team> GetAllTeams1()
        {
            if (teams.Count == 0)
            {
                teams.Add(
                    new Team(1, "Real", 1, Spain, ParseGroup("A")));
                teams.Add(
                   new Team(2, "Barcelone", 1, Spain, ParseGroup("A")));
                teams.Add(
                   new Team(3, "Leicester", 1, England, ParseGroup("A")));
                teams.Add(
                   new Team(4, "Bayern", 1, Germany, ParseGroup("A")));
                teams.Add(
                   new Team(5, "Juventus", 1, Italy, ParseGroup("A")));
                teams.Add(
                   new Team(6, "Benfica", 1, Portugal, ParseGroup("A")));
                teams.Add(
                   new Team(7, "PSG", 1, France, ParseGroup("A")));
                teams.Add(
                   new Team(8, "CSKA", 1, Russia, ParseGroup("A")));
                teams.Add(new Team(9, "Atlético Madrid", 2, Spain, ParseGroup("B")));
                teams.Add(new Team(10, "Borussia Dortmund", 2, Germany, ParseGroup("B")));
                teams.Add(new Team(11, "Arsenal FC", 2, England, ParseGroup("B")));
                teams.Add(new Team(12, "Manchester City", 2, England, ParseGroup("B")));
                teams.Add(new Team(13, "Séville FCC3", 2, Spain, ParseGroup("B")));
                teams.Add(new Team(14, "FC Porto", 2, Portugal, ParseGroup("B")));
                teams.Add(new Team(15, "SSC Naples", 2, Italy, ParseGroup("B")));
                teams.Add(new Team(16, "Bayer Leverkusen", 2, Germany, ParseGroup("B")));
                teams.Add(new Team(17, "FC Bâle C", 3, Switzerland, ParseGroup("C")));
                teams.Add(new Team(18, "Tottenham Hotspur", 3, England, ParseGroup("C")));
                teams.Add(new Team(19, "Dynamo Kiev C", 3, Ukraine, ParseGroup("C")));
                teams.Add(new Team(20, "Olympique lyonnais", 3, France, ParseGroup("C")));
                teams.Add(new Team(21, "PSV EindhovenC", 3, Netherlands, ParseGroup("C")));
                teams.Add(new Team(22, "Sporting CP", 3, Portugal, ParseGroup("C")));
                teams.Add(new Team(23, "Club Bruges C", 3, Belgium, ParseGroup("C")));
                teams.Add(new Team(24, "Borussia Mönchengladbach", 3, Germany, ParseGroup("C")));
                teams.Add(new Team(25, "Celtic Glasgow C", 4, Scotland, ParseGroup("D")));
                teams.Add(new Team(26, "AS Monaco", 4, France, ParseGroup("D")));
                teams.Add(new Team(27, "Beşiktaş JK C", 4, Turkey, ParseGroup("D")));
                teams.Add(new Team(28, "Legia Varsovie C", 4, Poland, ParseGroup("D")));
                teams.Add(new Team(29, "Dinamo Zagreb C", 4, Croatia, ParseGroup("D")));
                teams.Add(new Team(30, "Ludogorets Razgrad C", 4, Bulgaria, ParseGroup("D")));
                teams.Add(new Team(31, "FC Copenhague C", 4, Danemark, ParseGroup("D")));
                teams.Add(new Team(32, "FK Rostov", 4, Russia, ParseGroup("D")));

            }
            return teams;
        }

        public List<Team> GetAllTeams2()
        {
            if (teams.Count == 0)
            {
                teams.Add(
                    new Team(1, "Real", 1, Spain, ParseGroup("A")));
                teams.Add(
                   new Team(2, "PSG", 2, France, ParseGroup("A")));
                teams.Add(
                   new Team(3, "Wolfsburg", 1, Germany, ParseGroup("B")));
                teams.Add(
                   new Team(4, "PSV", 2, Netherlands, ParseGroup("B")));
                teams.Add(
                   new Team(5, "Atletico", 1, Spain, ParseGroup("C")));
                teams.Add(
                   new Team(6, "Benfica", 2, Portugal, ParseGroup("C")));
                teams.Add(
                   new Team(7, "Man City", 1, England, ParseGroup("D")));
                teams.Add(
                   new Team(8, "Juventus", 2, Italy, ParseGroup("D")));
                teams.Add(
                   new Team(9, "Barcelone", 1,Spain, ParseGroup("E")));
                teams.Add(
                   new Team(10, "Roma", 2, Italy, ParseGroup("E")));
                teams.Add(
                   new Team(11, "Bayern", 1, Germany, ParseGroup("F")));
                teams.Add(
                   new Team(12, "Arsenal", 2, England, ParseGroup("F")));
                teams.Add(
                   new Team(13, "Chelsea", 1, England, ParseGroup("G")));
                teams.Add(
                   new Team(14, "Dynamo kiev", 2, Ukraine, ParseGroup("G")));
                teams.Add(
                   new Team(15, "Zenith", 1,Russia, ParseGroup("H")));
                teams.Add(
                   new Team(16, "Gent", 2, Belgium, ParseGroup("H")));
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
