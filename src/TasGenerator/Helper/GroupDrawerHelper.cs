using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TasGenerator.Model;

namespace TasGenerator.Helper
{
    public class GroupDrawerHelper
    {
        public List<Checkable<Team>> DrawRules { get; set; }
        public int nonUniqueTries = 0;

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

            List<DrawGroup> groupModified = null;
            var currentSolution = new DrawSolution();
            var firstTeam = TakeTeamInGroup(groupsFull, out groupModified, 0, 0);
            var currentItem = new DrawItem();
            currentItem.Teams.Add(firstTeam);
            CompleteSolutions(groupModified, solutions, currentSolution, new DrawItem(currentItem), nbTeamsByItem, nbTeamsByGroups, 1);

            return solutions;
        }

        public ConcurrentDictionary<int, DrawSolution> ParallelMakeDraw(List<DrawGroup> groupsFull)
        {
            var solutions = new ConcurrentDictionary<int, DrawSolution>();
            var nbGroups = groupsFull.Count;
            var nbTeamsByItem = nbGroups;
            var nbTeamsByGroups = groupsFull[0].Teams.Count;

            List<DrawGroup> groupModified = null;
            var currentSolution = new DrawSolution();
            var firstTeam = TakeTeamInGroup(groupsFull, out groupModified, 0, 0);
            var currentItem = new DrawItem();
            currentItem.Teams.Add(firstTeam);
            ParallelCompleteSolutions(groupModified, solutions, currentSolution, new DrawItem(currentItem), nbTeamsByItem, nbTeamsByGroups, 1);

            return solutions;
        }


        public void CompleteSolutions(List<DrawGroup> groupsWithoutDrawItem, List<DrawSolution> solutions, DrawSolution currentSolution, DrawItem currentItem, int nbTeamsByItem, int nbItemBySolution, int level)
        {

            // Group to select  = level % nbGroup = level % nbTeamsBYItems
            var groupIndex = level % nbTeamsByItem;
            int teamIndex = 0;

            Action<int> process = (int tIndex) =>
            {
                // Take team and remove it in group, we make this in another group in order to make 
                List<DrawGroup> goupModifed = null;
                var team = TakeTeamInGroup(groupsWithoutDrawItem, out goupModifed, groupIndex, teamIndex);

                var canAddTeamToItem = DrawRules.All(rule => rule.Check(currentItem.Teams, team));
                if (canAddTeamToItem)
                {
                    // Clone item
                    var item = new DrawItem(currentItem);
                    item.Teams.Add(team);

                    // Save current item
                    var solution = new DrawSolution(currentSolution);

                    if (groupIndex == nbTeamsByItem - 1)
                    {
                        solution.AddItem(item);
                        item = new DrawItem();
                    }

                    // Next
                    CompleteSolutions(CloneGroups(goupModifed), solutions, new DrawSolution(solution), new DrawItem(item), nbTeamsByItem, nbItemBySolution, level + 1);
                }
            };


            // No more teams, we have a solution
            if (level == nbTeamsByItem * nbItemBySolution)
            {
                // Store solution
                if (!solutions.Contains(currentSolution))
                    solutions.Add(currentSolution);
                else Interlocked.Increment(ref nonUniqueTries);

                // bye
                return;
            }
            else
            {
                // do not perform on last group
                if (groupIndex == nbTeamsByItem - 2)
                {
                    process(teamIndex);
                }
                else
                {
                    // For each solution in next subgroup
                    while (teamIndex < groupsWithoutDrawItem[groupIndex].Teams.Count)
                    {
                        process(teamIndex);
                        teamIndex++;
                    }
                }
            }
        }

        public void ParallelCompleteSolutions(List<DrawGroup> groupsWithoutDrawItem, ConcurrentDictionary<int, DrawSolution> solutions, DrawSolution currentSolution, DrawItem currentItem, int nbTeamsByItem, int nbItemBySolution, int level)
        {
            if (level == nbTeamsByItem * nbItemBySolution)
            {
                // No more teams to choose
                // Save solution
                if (!solutions.ContainsKey(currentSolution.GetHashCode()))
                    solutions.TryAdd(currentSolution.GetHashCode(), currentSolution);
                //   currentSolution = new DrawSolution();
                // bye
                return;
            }
            else
            {
                // Group to select  = level % nbGroup = level % nbTeamsBYItems
                var groupIndex = level % nbTeamsByItem;

                // For each solution in next subgroup
                Parallel.For(0, groupsWithoutDrawItem[groupIndex].Teams.Count,
                    (index) =>
                    {
                        // Take team and remove it in group
                        List<DrawGroup> goupModifed = null;
                        var team = TakeTeamInGroup(groupsWithoutDrawItem, out goupModifed, groupIndex, index);
                        var item = new DrawItem(currentItem);
                        item.Teams.Add(team);

                        // Save current item
                        var solution = new DrawSolution(currentSolution);

                        if (groupIndex == nbTeamsByItem - 1)
                        {
                            solution.AddItem(item);
                            item = new DrawItem();
                        }
                        // Next
                        ParallelCompleteSolutions(CloneGroups(goupModifed), solutions, new DrawSolution(solution), new DrawItem(item), nbTeamsByItem, nbItemBySolution, level + 1);

                    }
                    );
            }
        }

        public Team TakeTeamInGroup(List<DrawGroup> groupsWithoutDrawItem, out List<DrawGroup> groupModifed, int groupIndex, int teamIndex)
        {
            groupModifed = CloneGroups(groupsWithoutDrawItem);
            var firstTeam = groupsWithoutDrawItem[groupIndex].Teams[teamIndex];
            groupModifed[groupIndex].Teams.RemoveAt(teamIndex);
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
