using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TourPlanner.DataAccessLayer;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    public class TourFactoryImpl : ITourFactory
    {
        private readonly Database db; 
        public TourFactoryImpl()
        {
            db = Database.getInstance(); 
        }
        public IEnumerable<Tour> GetItems()
        {
            List<Tour> tourlist = db.GetAllTours();

            return tourlist;

        }

        public IEnumerable<Tour> SearchInDB(string itemName)
        {
           IEnumerable<Tour> tourlist = GetItems();


            return db.SearchText(itemName.ToLower()); 

            //return tourlist.Where(x => x.Name.ToLower().Contains(itemName.ToLower()) || x.TourDescription.ToLower().Contains(itemName.ToLower()));
        }

        public IEnumerable<Tour> DeleteTour(IEnumerable<Tour> OldTours, int index)
        {
            ObservableCollection<Tour> NewTours = new ObservableCollection<Tour>(OldTours);
            NewTours.RemoveAt(index);
            return NewTours;
        }

        public Tour AddTourToDB(string name, string tourdescription, string tourfrom, string tourto, TransportType transporttype, string distance, string estimatedtime)
        {
            db.AddTour(name, tourdescription, tourfrom, tourto, transporttype, distance, estimatedtime);

            return db.GetNewestTour();

        }
        public void DeleteTourFromDB(int id)
        {
            db.DeleteTour(id);
        }

        public Tour UpdateTourInDB(Tour tour)
        {
            db.UpdateTour(tour);
            return tour = Database.getInstance().GetTourById(tour.Id);
        }

        public Tourlog AddTourlogToDB(int tourid, string date, string comment, int difficulty, string totaltime, int rating)
        {
            db.AddTourlog(tourid, date, comment, difficulty, totaltime, rating);

            return db.GetNewestTourlog(); 
        }

        public ObservableCollection<Tourlog> GetTourlogsByIdFromDB(int tourid)
        {
            return db.GetTourlogsByTourId(tourid);
        }

        public void DeleteTourlogFromDB(int id)
        {
            db.DeleteTourlog(id);
        }

        public Tourlog UpdateTourlogInDB(Tourlog tourlog)
        {
            db.UpdateTourlog(tourlog);
            return Database.getInstance().GetTourlogByLogId(tourlog.Id); 
        }
    }
}