using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasGenerator.Model
{
    public class DrawGroup
    {
        public string GroupIdentifier { get; set; }
        public List<Team> Teams { get; set; }

        public DrawGroup()
        {

        }

        public DrawGroup(DrawGroup group)
        {
            GroupIdentifier = group.GroupIdentifier;
            Teams = new List<Model.Team>(group.Teams);
        }
    }
}
