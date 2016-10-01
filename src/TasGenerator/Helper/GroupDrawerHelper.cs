using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasGenerator.Model;

namespace TasGenerator.Helper
{
    public class GroupDrawerHelper
    {
        public List<Checkable<Team>> DrawRules { get; set; }

        public GroupDrawerHelper(List<Checkable<Team>> drawRules)
        {
            DrawRules = drawRules;
        }


        public List<DrawSolution> MakeDraw(List<DrawGroup> groupsFull)
        {
            var solutions = new List<DrawSolution>();
            var nbGroups = groupsFull.Count;
            var nbTeamsByItem = nbGroups;
            var nbTeamsByGroups = groupsFull[0].Teams.Count;

            for (int i = 0; i < nbTeamsByGroups; i++)
            {
                Team team = groupsFull[0].Teams[i];

            }

            var firstTeam = groupsFull[groupToSwift].Teams.First();
            groupsFull[groupToSwift].Teams.RemoveAt(0);
            groupsFull[groupToSwift].Teams.Add(firstTeam);  
        }

        public void CompleteSolution(List<DrawGroup> groupsWithoutDrawItem, DrawSolution currentSolution, DrawItem currentItem, int nbTeamsByItem, int groupIndex, int nbItemBySolution = 0)
        {
            if (currentSolution == null)
                currentSolution = new DrawSolution();

            //int i = 0;
            //while (currentItem.Teams.Count < nbTeamsByItem)
            //{
            //    var team = TakeTeamInGroup(groupsWithoutDrawItem, i);
            //    i++;
            //    currentItem.Teams.Add(team);
            //}

            if (currentSolution.DrawItems.Count < nbItemBySolution)
            {
                var team = TakeTeamInGroup(groupsWithoutDrawItem, groupIndex);
                CompleteDrawItem(groupsWithoutDrawItem, currentSolution, currentItem, nbTeamsByItem, groupIndex + 1);
            }
            else
                currentSolution.AddItem(currentItem);
        }


        public void CompleteDrawItem(List<DrawGroup> groupsWithoutDrawItem, DrawSolution currentSolution, DrawItem currentItem, int nbTeamsByItem, int groupIndex, int teamIndex = 0)
        {
            if (currentItem == null)
                currentItem = new DrawItem();

            //int i = 0;
            //while (currentItem.Teams.Count < nbTeamsByItem)
            //{
            //    var team = TakeTeamInGroup(groupsWithoutDrawItem, i);
            //    i++;
            //    currentItem.Teams.Add(team);
            //}

            if (currentItem.Teams.Count < nbTeamsByItem)
            {
                var team = TakeTeamInGroup(groupsWithoutDrawItem, groupIndex);
                CompleteDrawItem(groupsWithoutDrawItem, currentSolution,currentItem, nbTeamsByItem, groupIndex + 1);
            }
            else
                currentSolution.AddItem(currentItem);
        }

        public Team TakeTeamInGroup(List<DrawGroup> groupsWithoutDrawItem, int groupIndex)
        {
            var firstTeam = groupsWithoutDrawItem[groupIndex].Teams.First();
            groupsWithoutDrawItem[groupIndex].Teams.RemoveAt(0);
            return firstTeam;
        }


        public DrawSolution GenerateDrawSolution(List<DrawGroup> groupsFull)
        {
            var drawSolution = new DrawSolution();
            int numberOfItems = groupsFull[0].Teams.Count;
            for (var i = 0; i < numberOfItems; i++)
            {
                var drawItem = GenerateDrawItem(groupsFull);
                if (drawItem == null)
                    //solution not possible
                    return null;
                drawSolution.DrawItems.Add(drawItem);
            }
            return drawSolution;
        }

        static public List<DrawGroup> CloneGroups(List<DrawGroup> groupsFull)
        {
            var newGroups = new List<DrawGroup>();
            foreach (var group in groupsFull)
            {
                newGroups.Add(new DrawGroup(group));
            }
            return newGroups;
        }

        public DrawItem GenerateDrawItem(List<DrawGroup> groupsWithoutDrawItem)
        {
            var item = new DrawItem();

            return item;
        }

        //private Team PickAndCheckATeam(List<Checkable<Team>> drawRules, DrawGroup currentGroup, DrawItem currentDrawItem, List<Team> teamNotPossibleInCurrentGroup)
        //{

        //    var teamPossibleInGroup = currentGroup.Teams.Except(teamNotPossibleInCurrentGroup);
        //    if (teamPossibleInGroup.Any())
        //    {
        //        foreach (var teamToCheck in teamPossibleInGroup)
        //        {
        //            if (drawRules.All(rule => rule.Check(currentDrawItem.Teams, teamToCheck)))
        //            {
        //                return teamToCheck;
        //            }
        //        }
        //    }
        //    return null;
        //}

    }
}
