using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasGenerator.Model;

namespace TasGenerator.Helper
{
    public class SameGroupRule : Checkable<Team>
    {
        public override bool Check(Team first, Team second)
        {
            if (first == null || second == null)
                return false;
            if (first.Group == null || second.Group == null)
                return false;
            return first.Group.Name != second.Group.Name;
        }

     
    }

    public  class SameRankRule : Checkable<Team>
    {
        public override bool Check(Team first, Team second)
        {
            if (first == null || second == null)
                return false;
            return first.Rank != second.Rank;
        }
    
    }


    public  class SameCountryRule : Checkable<Team>
    {
        public override bool Check(Team first, Team second)
        {
            if (first == null || second == null)
                return false;
            if (first.Country == null || second.Country == null)
                return false;
            return first.Country != second.Country;
        }
   
    }

    public  class WarRule : Checkable<Team>
    {
        public List<string> Belligerants = new List<string>();

        public override bool Check(Team first, Team second)
        {
            if (first == null || second == null)
                return false;
            if (first.Country == null || second.Country == null)
                return false;
            return !(Belligerants.Contains(first.Country) && Belligerants.Contains(second.Country));
        }
     
    }

}
