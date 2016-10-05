using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasGenerator.Helper;
using TasGenerator.Model;

namespace TasGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var competition = new Competition()
            {
                Name = "Champions League",
                Season = Season.SEASON_2015_2016
            };

            competition.Draws.Add(new DrawInfo()
            {
                Name = "8eme",
                Rules = "Not same country, not same group, 1st vs 2nd, Zenith can't match Kiev",
                DrawDetail = new DrawDetail(),
                DrawRules = new List<Checkable<Team>>()
                {
                    new SameCountryRule(),
                    new SameGroupRule(),
                    new SameRankRule(),
                    new WarRule()
                    {
                        Belligerants = new List<string>() { "Russia","Ukraine" }
                    }
                }
            }

               );

            var groups = DrawHelper.Instance.GetDrawGroupsByRank();

            var helper = new GroupDrawerHelper(competition.Draws[0].DrawRules);

            //var testGroups = new List<DrawGroup>()
            //{
            //    new DrawGroup()
            //    {
            //        GroupIdentifier = "1",
            //        Teams = new List<Team>() { new Team(1,"1",1,"1","A"),
            //        new Team(2,"2",2,"2","A")
            //                 ,new Team(3,"3",3,"3","A")
            //        }
            //    }
            //    ,  new DrawGroup()
            //    {
            //        GroupIdentifier = "2",
            //        Teams = new List<Team>() { new Team(4,"a",2,"a","B"),
            //        new Team(5,"b",2,"b","B")
            //               ,new Team(6,"c",2,"c","B")
            //        }
            //    }
            //      ,new DrawGroup()
            //    {
            //        GroupIdentifier = "3",
            //       Teams = new List<Team>() { new Team(7,"u",3,"u","C"),
            //        new Team(8,"v",3,"v","C")
            //         ,new Team(9,"w",3,"w","C")}
            //    }

            //};

            //   var  solutions = helper.ShiftAllTeamAndDrawSolutions(LineGroupDrawerHelper.CloneGroups(groups), groups[0].Teams.Count, 0);
            var solutions = helper.ParallelMakeDraw(GroupDrawerHelper.CloneGroups(groups));
            foreach (var sol in solutions.Values)
            {
                sol.PrintSolution();
            }
            //DrawSolution current = new DrawSolution(groups.Count);
            //var currentDraw = competition.Draws[0];
            //DrawItem currentItem = new DrawItem(groups.Count);
            //var allSolutions = new ConcurrentBag<DrawSolution>();
            //var listOfTeammUnvailabe = new List<Team>[groups.Count];

            //int i = -1;
            //// var resultItem = DrawHelper.Instance.FillNextTeamInItem(i, currentDraw.DrawRules, groups,ref currentItem, listOfTeammUnvailabe);

            //var sol = DrawHelper.Instance.ComputeSolution(currentDraw, groups);
            //Console.WriteLine("==============================================");
            //Console.WriteLine("====Group ");
            //foreach (var resultItem in sol.DrawItems)
            //    Console.WriteLine(resultItem.ToString());
            //Console.WriteLine("==============================================");

            //  competition.Draws[0].GroupAndNext(groups, current, currentItem, allSolutions);

            Console.WriteLine("==============================================");
            Console.WriteLine("====Number of solution " + solutions.Count);
            Console.WriteLine("==============================================");

            var barcPSG = DrawSolution.NumberOfDrawItemInCommon(solutions.Values.ToList(), "Barcelone", "PSG");

            var pecentage = (float)barcPSG * 100 / solutions.Count;
            Console.WriteLine("==============================================");
            Console.WriteLine("====Percentage of Barca PSG (" + barcPSG + " over " + solutions.Count + ") " + pecentage + "%");
            Console.WriteLine("==============================================");

            var PSVpsg = DrawSolution.NumberOfDrawItemInCommon(solutions.Values.ToList(), "Man City", "PSG");

            pecentage = (float)PSVpsg * 100 / solutions.Count;
            Console.WriteLine("==============================================");
            Console.WriteLine("====Percentage of Man City PSG (" + PSVpsg + " over " + solutions.Count + ") " + pecentage + "%");
            Console.WriteLine("==============================================");


            ////foreach (var d in competition.Draws[0].DrawDetail.MatchSolution)
            ////    d.PrintSolution();
            //competition.Draws[0].PrintPercetageSecond( "PSG");
            //competition.Draws[0].PrintPercetageFirst("Zenith");
            ////PrintPercetageSecond(allSolutions.ToList(), "Roma");
            //PrintPercetageFirst(allSolutions.ToList(), "Bayern");

            //    competition.Save().Wait();
            // zenith.Save().Wait();
            Console.ReadLine();
        }

        //static void Main(string[] args)
        //{


        //    var competition = new Competition()
        //    {
        //        Name = "Champions League",
        //        Season = Season.SEASON_2015_2016,

        //    };
        //    competition.Draws.Add(new DrawInfo()
        //    {
        //        Name = "8eme",
        //        Rules = "Not same country, not same group, 1st vs 2nd, Zenith can't match Kiev",
        //        DrawDetail = new DrawDetail(),
        //        DrawRules = new List<Checkable<Team>>()
        //        {
        //            new SameCountryRule(),
        //            new SameGroupRule(),
        //            new SameRankRule(),
        //            new WarRule()
        //            {
        //                Belligerants = new List<string>() { "Russia","Ukraine" }
        //            }
        //        }  
        //    }

        //       );


        //    var teams= DrawHelper.Instance.GetAllTeams();
        //    var zenith = teams.SingleOrDefault(t => t.Name == "Zenith");
        //    var kiev = teams.SingleOrDefault(t => t.Name == "Dynamo kiev");
        //    competition.Draws[0].Teams = teams;
        //    competition.Draws[0].SpecialExclusions.Add(new SpecialExclusion() { FirstRank = zenith, SecondRank = kiev });

        //    Console.ReadLine();
        //    Console.Clear();
        //    Console.WriteLine("==============================================");
        //    // 
        //    var teamsFirst = competition.Draws[0].GetFirstTeamsAvailable();
        //    var allSolutions = new ConcurrentBag<DrawSolution>();
        //    var nextFirst = teamsFirst.Keys.FirstOrDefault();
        //    Parallel.ForEach(teamsFirst[nextFirst],
        //    (teamSecond) =>
        //    {
        //    //    foreach (var teamSecond in teamsFirst[nextFirst])
        //    //{
        //        var soluiton = new DrawSolution(2);
        //        DrawHelper.Instance.AssociateAndContinue(teamsFirst, nextFirst, teamSecond, ref soluiton, allSolutions);
        //        // 
        //        //if (!allSolutions.Contains(soluiton))
        //        //    allSolutions.Add(soluiton);
        //        Console.WriteLine("==============================================");
        //        Console.WriteLine(allSolutions.Count);
        //        Console.WriteLine("==============================================");
        //        //}
        //    });



        //    Console.WriteLine("==============================================");
        //    Console.WriteLine("====Number of solution " + allSolutions.Count);
        //    Console.WriteLine("==============================================");
        //    var allSolutionsAsList = allSolutions.Distinct().ToList();

        //    competition.Draws[0].DrawDetail.MatchSolution = allSolutionsAsList;


        //    //Console.WriteLine("==============================================");
        //    //Console.WriteLine("====Number of solution " + allSolutionsAsList.Count);
        //    //Console.WriteLine("==============================================");

        //    //var barcPSG = MatchSolution.NumberOfMatchBetween(competition.Draws[0].DrawDetail.MatchSolution, "Barcelone", "PSG");
        //    ////  var barcPSG = allSolutions.Count(c => c.Matches.Any(f => f.FirstRank.Name == "Barcelone" && f.SecondRank.Name == "PSG"));

        //    //var pecentage = (float)barcPSG * 100 / competition.Draws[0].DrawDetail.MatchSolution.Count;
        //    //Console.WriteLine("==============================================");
        //    //Console.WriteLine("====Percentage of Barca PSG (" + barcPSG + " over " + competition.Draws[0].DrawDetail.MatchSolution.Count + ") " + pecentage + "%");
        //    //Console.WriteLine("==============================================");

        //    ////foreach (var d in competition.Draws[0].DrawDetail.MatchSolution)
        //    ////    d.PrintSolution();
        //    //competition.Draws[0].PrintPercetageSecond( "PSG");
        //    //competition.Draws[0].PrintPercetageFirst("Zenith");
        //    ////PrintPercetageSecond(allSolutions.ToList(), "Roma");
        //    //PrintPercetageFirst(allSolutions.ToList(), "Bayern");

        //     competition.Save().Wait();
        //   // zenith.Save().Wait();
        //    Console.ReadLine();
        //}

    }
}
