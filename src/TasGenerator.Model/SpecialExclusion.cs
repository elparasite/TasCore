using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasGenerator.Model
{
    public class SpecialExclusion
    {
        public int Id { get; set; }
        public Team FirstRank { get; set; }
        public Team SecondRank { get; set; }
    }
}
