using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasGenerator.Model;

namespace TasGenerator.Helper
{
    public class DrawStat
    {
        public Team FirstTeam { get; set; }
        public Team SecondTeam
        { get; set; }

        public string FirstTeamName { get { return FirstTeam.Name; } }
        public string SecondTeamName { get { return SecondTeam.Name; } }
      
        public float Probability { get; set; }
    }

    public sealed class DrawStatMap : CsvClassMap<DrawStat>
    {
        public DrawStatMap()
        {
            Map(m => m.FirstTeamName).Name("First");
            Map(m => m.SecondTeamName).Name("Second");
            Map(m => m.Probability);
        }
    }

}
