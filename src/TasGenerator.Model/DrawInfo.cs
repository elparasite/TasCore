using System.Collections.Generic;
using System.Linq;

namespace TasGenerator.Model
{
    public class DrawInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Rules { get; set; }
        public int NumberOfTeamsByDrawSolution { get; set; } = 2;
        public DrawDetail DrawDetail { get; set; }
        public List<Checkable<Team>> DrawRules { get; set; }


        public enum TASStatus
        {
            DrawItemOver,
            Continue,
            DrawSolutionOver,
            DeadEnd,
            NotPossible

        }

        public List<SpecialExclusion> SpecialExclusions { get; set; } = new List<SpecialExclusion>();

        public List<Team> Teams { get; set; }

        public Dictionary<Team, List<Team>> GetTeamsAvailable()
        {
            var result = new Dictionary<Team, List<Team>>();

            foreach (var team in Teams)
            {
                var excludeteam = SpecialExclusions
                    .Where(te => (team.Rank == 1 && te.FirstRank == team) || (team.Rank == 2 && te.SecondRank == team))
                    .Select(t =>
                    {
                        if (team.Rank == 1) return t.SecondRank;
                        else return t.FirstRank;
                    }
                    ).ToList();

                //result.Add(team, new Stack<Team>(allTeams.Where(t => t.Rank != team.Rank
                //    && t.Country != team.Country 
                //    && t.Group.Name != team.Group.Name).ToList()));
                result.Add(team, Teams.Where(t => t.Rank != team.Rank
                && t.Country != team.Country
                && t.Group.Name != team.Group.Name
                && !excludeteam.Contains(t)).ToList());
            }
            return result;
        }



        internal Dictionary<Team, List<Team>> GetSecondTeamsAvailable()
        {
            var result = new Dictionary<Team, List<Team>>();
            return GetTeamsAvailable().Where(kp => kp.Key.Rank == 2).ToDictionary(g => g.Key, g => g.Value);
        }

        public Dictionary<Team, List<Team>> GetFirstTeamsAvailable()
        {
            var result = new Dictionary<Team, List<Team>>();
            return GetTeamsAvailable().Where(kp => kp.Key.Rank == 1).ToDictionary(g => g.Key, g => g.Value);
        }

   
        internal Dictionary<Team, int> ComputePercentage(string teamName)
        {
            var listOfPercent = new Dictionary<Team, int>(Teams.Where(t => t.Name != teamName).ToDictionary(kp => kp, kp => 0));
            foreach (var sol in DrawDetail.MatchSolution)
            {
                foreach (var drawItem in sol.DrawItems)
                {
                    if (drawItem.Teams.Any(t => t.Name == teamName))
                        foreach (var opponent in drawItem.Teams.Where(t => t.Name != teamName))
                            listOfPercent[opponent]++;
                }
            }
            return listOfPercent;
        }

   
        public bool RemoveAndCheck(Dictionary<Team, List<Team>> team, Team current, Team opponentInAnotherMatch)
        {
            if (team[current].Contains(opponentInAnotherMatch))
            {
                team[current].Remove(opponentInAnotherMatch);
            }
            return team[current].Count > 0;
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
        //public TASStatus AssociateAndContinue(Dictionary<Team, List<Team>> teams, Team currentTeam, Team opponent, ref DrawSolution currentSolution, ConcurrentBag<DrawSolution> allSOlutions)
        //{
        //    // Add match to solution
        //    var currentMatch = new DrawItem(currentSolution.NumberTeamsByItems, currentTeam);

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
        //        return TASStatus.DrawSolutionOver;
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


        //public TASStatus GroupAndNext(List<DrawGroup> updajtedGroups, DrawSolution currentSolutionTeam, DrawItem currentDrawItem, ConcurrentBag<DrawSolution> allSOlutions)
        //{
        //    var newUpdatedGroups = new List<DrawGroup>(updajtedGroups);
            
        //    if (!newUpdatedGroups.Any())
        //    {
        //        // PLus de groupe donc tirage d'items fini
        //        currentSolutionTeam.AddMatch(currentDrawItem);
        //        currentDrawItem = new DrawItem(currentDrawItem.NumberTeamsByItems);
        //        return TASStatus.DrawItemOver;
        //    }
        //    else
        //    {
        //        var nextGroup = newUpdatedGroups.First();

        //        if (!nextGroup.Teams.Any())
        //        {
        //            // Plus d'équipe dans le (dernier) groupe, solutions fini
        //            if (!allSOlutions.Contains(currentSolutionTeam))
        //                allSOlutions.Add(currentSolutionTeam);
        //            currentSolutionTeam = new DrawSolution(0);
        //            return TASStatus.DrawSolutionOver;
        //        }
        //        else
        //        {
        //            var nextTeam = nextGroup.Teams.First();
        //            if (DrawRules.All(rule => rule.Check(currentDrawItem.Teams, nextTeam)))
        //            {
        //                // Team can match
        //                currentDrawItem.Teams.Add(nextTeam);
        //                var res = GroupAndNext(newUpdatedGroups, currentSolutionTeam, currentDrawItem, allSOlutions);
        //                switch (res)
        //                {
        //                    case TASStatus.DeadEnd:
        //                        updajtedGroups.Enqueue(nextGroup);
        //                       return GroupAndNext(newUpdatedGroups, currentSolutionTeam, currentDrawItem, allSOlutions))
        //                    case TASStatus.NotPossible:
        //                        return GroupAndNext(newUpdatedGroups, currentSolutionTeam, currentDrawItem, allSOlutions);
        //                    case TASStatus.DrawItemOver:
        //                        //currentSolutionTeam[0]
        //                        newUpdatedGroups.Enqueue(nextGroup);
        //                        if (newUpdatedGroups.Count < currentDrawItem.NumberTeamsByItems)
        //                                return TASStatus.DrawItemOver;
        //                        return GroupAndNext(newUpdatedGroups, currentSolutionTeam, currentDrawItem, allSOlutions);
        //                        case TASStatus.DrawSolutionOver:
        //                        return TASStatus.DrawSolutionOver;

        //                }


        //            }
        //            else
        //            {
        //                newUpdatedGroups.Enqueue(nextGroup);
        //                return TASStatus.NotPossible;
        //            }


        //        }
        //    }


        //    return TASStatus.DeadEnd;
        //}


    }



}
