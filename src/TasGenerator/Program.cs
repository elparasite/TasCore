using CsvHelper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
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
                        Belligerants = new List<Country>() { DrawHelper.Russia,DrawHelper.Ukraine }
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
            //        Teams = new List<Team>() { new Team(1,"Real madrid",1,DrawHelper.Spain),
            //        new Team(2,"2",1,"2","A")
            //                 ,new Team(3,"3",1,"3","A")
            //                 ,new Team(4,"4",1,"4","A")
            //        }
            //    }
            //    ,  new DrawGroup()
            //    {
            //        GroupIdentifier = "2",
            //        Teams = new List<Team>() { new Team(10,"a",2,"a","B"),
            //        new Team(11,"b",2,"b","B")
            //               ,new Team(12,"c",2,"c","B")
            //                ,new Team(13,"d",2,"d","B")
            //                //,new Team(14,"e",2,"e","B")
            //                //  ,new Team(15,"f",2,"f","B")
            //                //  ,new Team(16,"g",2,"g","B")
            //        }
            //    }
            //      ,new DrawGroup()
            //    {
            //        GroupIdentifier = "3",
            //       Teams = new List<Team>() { new Team(7,"u",3,"u","C"),
            //        new Team(8,"v",3,"v","C")
            //         ,new Team(9,"w",3,"w","C")
            //         ,new Team(6,"x",3,"x","C")
            //    }
            //      }
            //};

            var solutions = helper.MakeDraw(GroupDrawerHelper.CloneGroups(groups));
   
            // Test
            //Console.WriteLine($"Non unique tries : {helper.nonUniqueTries}");
            //foreach (var sol in solutions)
            //{
            //    sol.PrintSolution();
            //}

            Console.WriteLine("==============================================");
            Console.WriteLine("====Number of solution " + solutions.Count);
            Console.WriteLine("==============================================");

            //var barcPSG = DrawSolution.NumberOfDrawItemInCommon(solutions.ToList(), "Barcelone", "PSG");

            //var pecentage = (float)barcPSG * 100 / solutions.Count;
            //Console.WriteLine("==============================================");
            //Console.WriteLine("====Percentage of Barca PSG (" + barcPSG + " over " + solutions.Count + ") " + pecentage + "%");
            //Console.WriteLine("==============================================");

            //var PSVpsg = DrawSolution.NumberOfDrawItemInCommon(solutions.ToList(), "Man City", "PSG");

            //pecentage = (float)PSVpsg * 100 / solutions.Count;
            //Console.WriteLine("==============================================");
            //Console.WriteLine("====Percentage of Man City PSG (" + PSVpsg + " over " + solutions.Count + ") " + pecentage + "%");
            //Console.WriteLine("==============================================");

            //var barca = DrawHelper.Instance.GetAllTeams().Single(t => t.Name == "Barcelone");
            //var arsenal = DrawHelper.Instance.GetAllTeams().Single(t => t.Name == "Arsenal");

            //var matchDrawed = new List<DrawItem>();
            //matchDrawed.Add(new DrawItem()
            //    .AddTeam(barca)
            //    .AddTeam(arsenal));

            //solutions = solutions.Where(s => matchDrawed.All(drawedItem => s.DrawItems.Contains(drawedItem))).ToList();

            //Console.WriteLine("==============================================");
            //Console.WriteLine("====Number of solution " + solutions.Count);
            //Console.WriteLine("==============================================");

            List<DrawStat> stats = new List<DrawStat>();
            Console.WriteLine("==============================================");
            foreach (var first in DrawHelper.Instance.GetAllTeams().Where(t => t.Rank == 1))
            {
                foreach (var second in DrawHelper.Instance.GetAllTeams().Where(t => t.Rank == 2))
                {
                    var barcPSG = DrawSolution.NumberOfDrawItemInCommon(solutions.ToList(), first.Name, second.Name);
                    var pecentage = (float)barcPSG * 100 / solutions.Count;
                    stats.Add(new DrawStat()
                    {
                        FirstTeam = first,
                        SecondTeam = second,
                        Probability = pecentage

                    });

                }
             
             //   Console.WriteLine($"====Percentage of PSG vs {second.Name} (" + barcPSG + " over " + solutions.Count + ") " + pecentage + "%");
              
            }

            using (TextWriter writer = File.CreateText("test.csv"))
            {
                var csv = new CsvWriter(writer);
                csv.Configuration.Delimiter = ";";
                csv.Configuration.RegisterClassMap<DrawStatMap>();
                csv.WriteRecords(stats);
            }

            Console.WriteLine("==============================================");
            // pecentage = (float)barcPSG * 100 / solutions.Count;
            //Console.WriteLine("==============================================");
            //Console.WriteLine("====Percentage of Barca PSG (" + barcPSG + " over " + solutions.Count + ") " + pecentage + "%");
            //Console.WriteLine("==============================================");

            // PSVpsg = DrawSolution.NumberOfDrawItemInCommon(solutions.ToList(), "Man City", "PSG");

            //pecentage = (float)PSVpsg * 100 / solutions.Count;
            //Console.WriteLine("==============================================");
            //Console.WriteLine("====Percentage of Man City PSG (" + PSVpsg + " over " + solutions.Count + ") " + pecentage + "%");
            //Console.WriteLine("==============================================");

            //var bayern = DrawHelper.Instance.GetAllTeams().Single(t => t.Name == "Bayern");
            //var kiev = DrawHelper.Instance.GetAllTeams().Single(t => t.Name == "Dynamo kiev");

            //matchDrawed.Add(new DrawItem()
            //    .AddTeam(bayern)
            //    .AddTeam(kiev));

            //solutions = solutions.Where(s => matchDrawed.All(drawedItem => s.DrawItems.Contains(drawedItem))).ToList();


            //Console.WriteLine("==============================================");
            //Console.WriteLine("====Number of solution " + solutions.Count);
            //Console.WriteLine("==============================================");

            // barcPSG = DrawSolution.NumberOfDrawItemInCommon(solutions.ToList(), "Barcelone", "PSG");

            // pecentage = (float)barcPSG * 100 / solutions.Count;
            //Console.WriteLine("==============================================");
            //Console.WriteLine("====Percentage of Barca PSG (" + barcPSG + " over " + solutions.Count + ") " + pecentage + "%");
            //Console.WriteLine("==============================================");

            // PSVpsg = DrawSolution.NumberOfDrawItemInCommon(solutions.ToList(), "Man City", "PSG");

            //pecentage = (float)PSVpsg * 100 / solutions.Count;
            //Console.WriteLine("==============================================");
            //Console.WriteLine("====Percentage of Man City PSG (" + PSVpsg + " over " + solutions.Count + ") " + pecentage + "%");
            //Console.WriteLine("==============================================");


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

     
    }
}
