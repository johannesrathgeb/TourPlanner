using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
    public class Tour
    {
        public string Name { get; set; } 
        public int Id { get; set; }
        public string TourDescription { get; set; }
        public string From { get; set; }
        public string To { get; set; } 
        public TransportType TransportType { get; set; }
        public string TourDistance { get; set; }
        public string EstimatedRoute { get; set; }



        public Tour(string name, int id, string tourdescription, string from, string to, TransportType transporttype , string tourdistance, string estimatedroute)
        {
            Name = name;
            Id = id; 
            TourDescription = tourdescription;
            From = from;
            To = to;
            TransportType = transporttype;
            TourDistance = tourdistance;
            EstimatedRoute = estimatedroute;
        }

    }
}
