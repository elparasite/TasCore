using System.Collections.Generic;

namespace TasGenerator.Model
{
    public class DrawDetail
    {
        public int Id { get; set; }

        public List<DrawSolution> MatchSolution { get; set; } = new List<DrawSolution>();      
    }
}
