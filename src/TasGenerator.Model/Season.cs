using System;

namespace TasGenerator.Model
{
    public class Season
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }


        public static Season SEASON_2015_2016 = new Season() { Name = "2015-2016", Start = new DateTime(2015, 07, 01), End = new DateTime(2016, 06, 30) };
        public static Season SEASON_2016_2017 = new Season() { Name = "2016-2017", Start = new DateTime(2016, 07, 01), End = new DateTime(2017, 06, 30) };
    }
}
