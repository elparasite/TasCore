using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TasGenerator.Helper;
using TasGenerator.Model;

namespace TasGenerator
{

    class DrawHelper
    {
        private static DrawHelper instance;

        private DrawHelper() { }

        public static DrawHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DrawHelper();
                }
                return instance;
            }
        }

        public List<Checkable<Team>> Rules { get; set; } = new List<Checkable<Team>>();

        private List<Team> teams = new List<Team>();

        public List<Team> GetAllTeams()
        {
            if (teams.Count == 0)
            {
                teams.Add(
                    new Team(1,"Real", 1, "Spain", "A"));
                teams.Add(
                   new Team(2,"PSG", 2, "France", "A"));
                teams.Add(
                   new Team(3,"Wolfsburg", 1, "Germany", "B"));
                teams.Add(
                   new Team(4,"PSV", 2, "Netherlands", "B"));
                teams.Add(
                   new Team(5,"Atletico", 1, "Spain", "C"));
                teams.Add(
                   new Team(6,"Benfica", 2, "Portugal", "C"));
                teams.Add(
                   new Team(7,"Man City", 1, "England", "D"));
                teams.Add(
                   new Team(8,"Juventus", 2, "Italy", "D"));
                teams.Add(
                   new Team(9,"Barcelone", 1, "Spain", "E"));
                teams.Add(
                   new Team(10,"Roma", 2, "Italy", "E"));
                teams.Add(
                   new Team(11,"Bayern", 1, "Germany", "F"));
                teams.Add(
                   new Team(12,"Arsenal", 2, "England", "F"));
                teams.Add(
                   new Team(13,"Chelsea", 1, "England", "G"));
                teams.Add(
                   new Team(14,"Dynamo kiev", 2, "Ukraine", "G"));
                teams.Add(
                   new Team(15,"Zenith", 1, "Russia", "H"));
                teams.Add(
                   new Team(16,"Gent", 2, "Belgium", "H"));
            }
            return teams;
        }

        public List<DrawGroup> GetDrawGroupsByGroup()
        {
            return GetAllTeams().GroupBy(t => t.Group).Select(g => new DrawGroup()
            { GroupIdentifier = g.Key.Name, Teams = g.ToList() }).ToList();
        }


        public List<DrawGroup> GetDrawGroupsByRank()
        {
            return GetAllTeams().GroupBy(t => t.Rank).Select(g => new DrawGroup()
            { GroupIdentifier = g.Key.ToString(), Teams = g.ToList() }).ToList();
        }


        public bool RemoveAndCheck(Dictionary<Team, List<Team>> team, Team current, Team opponentInAnotherMatch)
        {
            if (team[current].Contains(opponentInAnotherMatch))
            {
                team[current].Remove(opponentInAnotherMatch);
            }
            return team[current].Count > 0;
        }

        public enum TASStatus
        {
            Continue,
            Stop,
            DeadEnd
        }

        private Dictionary<Team, List<Team>> CopyDictionary(Dictionary<Team, List<Team>> old)
        {
            var newDico = new Dictionary<Team, List<Team>>();
            foreach (var kp in old)
            {
                newDico.Add(kp.Key, kp.Value.ToList());
            }
            return newDico;
        }

        // old
        //public TASStatus AssociateAndContinue(Dictionary<Team, List<Team>> teams, Team currentTeam, Team opponent, ref DrawSolution currentSolution, ConcurrentBag<DrawSolution> allSOlutions)
        //{
        //    // Add match to solution
        //    var currentMatch = new DrawItem(currentSolution.NumberItems, currentTeam, opponent);
        //    currentSolution.AddMatch(currentMatch);

        //    // Pour chaque team restante on enelve l'opposant si dispo
        //    var teamDeleted = CopyDictionary(teams);
        //    foreach (var otherTeam in teams.Keys.Where(k => !k.Equals(currentTeam)))
        //    {
        //        // On supprime dans les autres listes 
        //        if (!RemoveAndCheck(teamDeleted, otherTeam, opponent))
        //        {
        //            //On se retrouve avec pas de solution
        //            // On vire le match et on recredite les équipes
        //            currentSolution.RemoveMatch(currentMatch);
        //            return TASStatus.DeadEnd;
        //        }
        //    }
        //    // All reomve is over
        //    teamDeleted.Remove(currentTeam);
        //    if (teamDeleted.Keys.Count == 0)
        //    {
        //        var nextSolution = new DrawSolution(currentSolution);
        //        nextSolution.RemoveMatch(currentSolution.DrawItems.Last());
        //        // Fini pou cet arbre de pssiblité
        //        if (!allSOlutions.Contains(currentSolution))
        //            allSOlutions.Add(currentSolution);
        //        currentSolution = nextSolution;
        //        return TASStatus.Stop;
        //    }
        //    var nextFirst = teamDeleted.Keys.FirstOrDefault();
        //    if (teamDeleted[nextFirst].Count == 0)
        //    {
        //        currentSolution.RemoveMatch(currentMatch);
        //        return TASStatus.DeadEnd;
        //    }
        //    foreach (var nextOpponenet in teamDeleted[nextFirst])
        //    {
        //        AssociateAndContinue(teamDeleted, nextFirst, nextOpponenet, ref currentSolution, allSOlutions);
        //    }

        //    // No Stop No more child, go level up
        //    currentSolution.RemoveMatch(currentMatch);
        //    return TASStatus.DeadEnd;
        //}


        private Team PickAndCheckATeam(List<Checkable<Team>> drawRules, DrawGroup currentGroup, DrawItem currentDrawItem, List<Team> teamNotPossibleInCurrentGroup)
        {

            var teamPossibleInGroup = currentGroup.Teams.Except(teamNotPossibleInCurrentGroup);
            if (teamPossibleInGroup.Any())
            {
                foreach (var teamToCheck in teamPossibleInGroup)
                {
                    if (drawRules.All(rule => rule.Check(currentDrawItem.Teams, teamToCheck)))
                    {
                        return teamToCheck;
                    }
                }
            }
            return null;
        }

        public DrawItem FillNextTeamInItem(int groupIndex, List<Checkable<Team>> drawRules, List<DrawGroup> groups, ref DrawItem currentDrawItem, List<Team>[] teamNotPossibleInCurrentGroup)
        {
            if (groups.Count == groupIndex + 1)
            {
                // No more groups => OK
                return currentDrawItem;
            }
            else
            {
                if (teamNotPossibleInCurrentGroup[groupIndex + 1] == null)
                    teamNotPossibleInCurrentGroup[groupIndex + 1] = new List<Team>();
                // ti+1
                var teamchoosen = PickAndCheckATeam(drawRules, groups[groupIndex + 1], currentDrawItem, teamNotPossibleInCurrentGroup[groupIndex + 1]);
                if (teamchoosen == null)
                {
                    // No solution in next group so remove current one and do again
                    // exclude current team 
                    teamNotPossibleInCurrentGroup[groupIndex].Add(currentDrawItem.Teams[groupIndex]);
                    currentDrawItem.Teams[groupIndex] = null;
                    if (groupIndex == 0)
                    {
                        // We didn't find any solution and we come back at the begiinngi of the goup list
                        // => So solution with current draw => END
                        return null;
                    }
                    else
                    {
                        // Again this level
                        return FillNextTeamInItem(groupIndex - 1, drawRules, groups, ref currentDrawItem, teamNotPossibleInCurrentGroup);
                    }
                }
                else
                {
                    // Add team and continue
                    currentDrawItem.Teams[groupIndex + 1] = teamchoosen;
                    // next
                    return FillNextTeamInItem(groupIndex + 1, drawRules, groups, ref currentDrawItem, teamNotPossibleInCurrentGroup);
                }
            }
        }

        public DrawSolution ComputeSolution(DrawInfo currentDrawInfo, List<DrawGroup> groupsForSOlution)
        {
            var groups = groupsForSOlution;
            var solution = new DrawSolution(groups[0].Teams.Count);
            var listOfTeamUnvailabe = new List<Team>[groups.Count];


            while (solution.DrawItems.Count < solution.NumberItems)
            {
                int i = -1;
                DrawItem currentItem = new DrawItem();
                var resultItem = FillNextTeamInItem(i, currentDrawInfo.DrawRules, groups, ref currentItem, listOfTeamUnvailabe);
                if (resultItem == null)
                {
                    // Dead end
                    var lastDrawItem = solution.DrawItems.Last();

                }
                else
                {
                    solution.AddItem(resultItem);
                    groups = RemoveTeams(resultItem, groups);
                }
            }

            return solution;
        }

        public List<DrawGroup> RemoveTeams(DrawItem teamsToRemove, List<DrawGroup> groups)
        {
            int i = 0;
            var result = new List<DrawGroup>();
            foreach (var group in groups)
            {
                group.Teams.Remove(teamsToRemove.Teams[i]);
                result.Add(group);
                i++;
            }
            return result;
        }

        //public List<DrawItem> AddTeamToItem(ref List<DrawSolution> currentSolutions, DrawItem currentItem,List<DrawGroup> groups, int level)
        //{
        //    if (currentItem == null)
        //        currentItem = new DrawItem();
        //    if (!groups.Any())
        //    {
        //        //// No more groups = a solution
        //        //var solution = new DrawSolution();
        //        //solution.AddMatch(currentItem);
        //        //currentSolution.Add(new DrawSolution() currentItem);
        //    }
        //    foreach (var team in groups[0].Teams)
        //    {
        //        currentItem.Teams[level] = team;
        //        AddTeamToItem(ref currentSolution,ref currentItem, groups.Skip(1).ToList(), level+1);

        //    }


        //}
    }
}
