using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasGenerator.Model
{
    public class DrawItem : IComparable
    {
        public List<Team> Teams { get; set; } = new List<Team>();
        public int NumberTeamsByItems { get; set; }

        //public bool IsValid
        //{
        //    get
        //    {
        //        return FirstRank.Country != SecondRank.Country
        //            && FirstRank.Group != SecondRank.Group
        //            && FirstRank.Rank != SecondRank.Rank;
        //    }

        public DrawItem()
        {
        }
        public DrawItem(DrawItem item)
        {
            NumberTeamsByItems = item.NumberTeamsByItems;
            Teams = new List<Team>(item.Teams);
        }


        public override bool Equals(object obj)
        {
            var matchB = obj as DrawItem;
            if (matchB == null) return false;
            return GetHashCode() == matchB.GetHashCode();
        }

        public override int GetHashCode()
        {
            return (ToString()).GetHashCode();
        }

        public override string ToString()
        {
            return string.Join(" - ", Teams.OrderByDescending(t => t.Rank).Select(tea => tea.ToString()).ToArray());
        }

        public int CompareTo(object obj)
        {
            var match2 = obj as DrawItem;
            return ToString().CompareTo(match2.ToString());

        }



    }
}
