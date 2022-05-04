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

        public Tour AddTourToDB(string name, string tourdescription, string tourfrom, string tourto, TransportType transporttype, string distance, string estimatedtime)
        {
            Database db = Database.getInstance();
            db.AddTour(name, tourdescription, tourfrom, tourto, transporttype, distance, estimatedtime);

            return db.GetNewestTour();

        }
        public void DeleteTourFromDB(int id)
        {
            Database.getInstance().DeleteTour(id);
        }
    }
}