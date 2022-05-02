using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TourPlanner.DataAccessLayer;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    internal class TourFactoryImpl : ITourFactory
    {
        public IEnumerable<Tour> GetItems()
        {
            Database db = Database.getInstance();

            List<Tour> tourlist = db.GetAllTours();

            return tourlist; 

            /*
            return new List<Tour>()
            {
               
                new Tour("Tour1", 1, "description1", "Salzburg1", "Wien1", TransportType.Car, "350km1", "1h"),
                new Tour("Tour2", 2, "description2", "Salzburg2", "Wien2", TransportType.Car, "350km2", "2h"),
                new Tour("Tour3", 3, "description3", "Salzburg3", "Wien3", TransportType.Car, "350km3", "3h"),
                new Tour("Tour4", 4, "description4", "Salzburg4", "Wien4", TransportType.Car, "350km4", "4h"),
                new Tour("Tour5", 5, "description5", "Salzburg5", "Wien5", TransportType.Car, "350km5", "5h")
             
            };
            */
        }

        public IEnumerable<Tour> Search(string itemName, bool caseSensitive = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tour> DeleteTour(IEnumerable<Tour> OldTours, int index)
        {
            ObservableCollection<Tour> NewTours = new ObservableCollection<Tour>(OldTours);
            NewTours.RemoveAt(index);
            return NewTours; 
        }

    }
}
