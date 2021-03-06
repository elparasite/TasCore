﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TasGenerator.Model
{
    public class DrawItem : IComparable
    {
        public int Id { get; set; }
        public List<Team> Teams { get; set; } = new List<Team>();
        public int NumberTeamsByItems { get; set; }


        public DrawItem()
        {
        }
        public DrawItem(DrawItem item)
        {
            NumberTeamsByItems = item.NumberTeamsByItems;
            Teams = new List<Team>(item.Teams);
        }

        public DrawItem AddTeam(Team team)
        {
            Teams.Add(team);
            return this;
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
