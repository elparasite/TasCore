using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasGenerator.Model;

namespace TasGenerator.Helper
{
    [Obsolete]
    public class LineGroupDrawerHelper
    {
        public List<Checkable<Team>> DrawRules { get; set; }

        public LineGroupDrawerHelper(List<Checkable<Team>> drawRules)
        {
            DrawRules = drawRules;
        }

        public void ShiftTeam(List<DrawGroup> groupsFull, int groupToSwift)
        {
            var firstTeam = groupsFull[groupToSwift].Teams.First();
            groupsFull[groupToSwift].Teams.RemoveAt(0);
            groupsFull[groupToSwift].Teams.Add(firstTeam);
        }


        public DrawSolution ShiftTeamAndDrawSolution(List<DrawGroup> groupsFull, int groupToSwift)
        {
            ShiftTeam(groupsFull, groupToSwift);
            var solution =  GenerateDrawSolution(CloneGroups(groupsFull));
            return solution;
        }


        public List<DrawSolution> ShiftAllTeamAndDrawSolutions(List<DrawGroup> groupsFull, int numberOfShift, int groupToSwift)
        {
            var solutions = new List<DrawSolution>();
            for (int i = 0; i < numberOfShift; i++)
            {
                ShiftTeam(groupsFull, groupToSwift);
                var solution = GenerateDrawSolution(CloneGroups(groupsFull));
                if (solution != null)
                    solutions.Add(solution);
            }
            return solutions;
        }

        public List<DrawSolution> ShiftAllTeamAllGroupAndDrawSolutions(List<DrawGroup> groupsFull, int numberOfGroups, int numberOfShift, int groupToSwift)
        {
            var solutions = new List<DrawSolution>();
            for (int i = 0; i < numberOfShift; i++)
            {
                // We shift
                ShiftTeam(groupsFull, groupToSwift);
                // We are on final group
                if (groupToSwift == numberOfGroups - 1)
                {
                    // No more groups
                    var solution = GenerateDrawSolution(CloneGroups(groupsFull));
                    if (solution != null)
                        solutions.Add(solution);
                }
                else
                {
                    var solty = ShiftAllTeamAllGroupAndDrawSolutions(groupsFull, numberOfGroups, numberOfShift, groupToSwift + 1);
                    solutions.AddRange(solty);
                }
            }
            return solutions;

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
            foreach (var group in groupsWithoutDrawItem)
            {
                var firstTeam = group.Teams.First();
                group.Teams.RemoveAt(0);
                if (!DrawRules.All(rule => rule.Check(item.Teams, firstTeam)))
                    return null;
                item.Teams.Add(firstTeam);
            }
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
