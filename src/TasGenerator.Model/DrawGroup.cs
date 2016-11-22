using System.Collections.Generic;

namespace TasGenerator.Model
{
    public class DrawGroup
    {
        public int Id { get; set; }
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
