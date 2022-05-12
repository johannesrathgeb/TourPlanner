using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
    public class Tourlog
    {
        public int Id { get; set; }
        public string Date { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public int Difficulty { get; set; }
        public string Totaltime { get; set; } = string.Empty;
        public int Rating { get; set; }

        public Tourlog(int id, string date, string comment, int difficulty, string totaltime, int rating)
        {
            Id = id;
            Date = date;
            Comment = comment;
            Difficulty = difficulty;
            Totaltime = totaltime;
            Rating = rating;
        }

        public Tourlog(string date, string comment, int difficulty, string totaltime, int rating)
        {
            Date = date;
            Comment = comment;
            Difficulty = difficulty;
            Totaltime = totaltime;
            Rating = rating;
        }

    }
}
