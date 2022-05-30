using System.Collections.ObjectModel;

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
        public string EstimatedTime{ get; set; }
        public int Popularity { get; set; }
        public int ChildFriendliness{ get; set; }
        public ObservableCollection<Tourlog> Tourlogs { get; set; } = new ObservableCollection<Tourlog>();



        public Tour(int id, string name, string tourdescription, string from, string to, TransportType transporttype , string tourdistance, string estimatedtime)
        {
            Id = id; 
            Name = name;
            TourDescription = tourdescription;
            From = from;
            To = to;
            TransportType = transporttype;
            TourDistance = tourdistance;
            EstimatedTime = estimatedtime;
        }

        public Tour(string name, string tourdescription, string from, string to, TransportType transporttype, string tourdistance, string estimatedtime)
        {
            Name = name;
            TourDescription = tourdescription;
            From = from;
            To = to;
            TransportType = transporttype;
            TourDistance = tourdistance;
            EstimatedTime = estimatedtime;
        }

        public Tour(string name, string tourdescription, string from, string to, TransportType transporttype)
        {
            Name = name;
            TourDescription = tourdescription;
            From = from;
            To = to;
            TransportType = transporttype;
        }

        public Tour(int id, string name, string tourdescription, string from, string to, TransportType transporttype)
        {
            Id = id; 
            Name = name;
            TourDescription = tourdescription;
            From = from;
            To = to;
            TransportType = transporttype;
        }

        public Tour()
        {

        }


    }
}
