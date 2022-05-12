using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    public interface ITourFactory
    {
        IEnumerable<Tour> GetItems();
        IEnumerable<Tour> Search(string itemName, bool caseSensitive = false);

        IEnumerable<Tour> DeleteTour(IEnumerable<Tour> Tours, int index);

        Tour AddTourToDB(string name, string tourdescription, string tourfrom, string tourto, TransportType transporttype, string distance, string time);
        Tour UpdateTourInDB(Tour tour);
        Tourlog UpdateTourlogInDB(Tourlog tourlog); 
        void DeleteTourFromDB(int id);
        void DeleteTourlogFromDB(int id); 
        Tourlog AddTourlogToDB(int tourid, string date, string comment, int difficulty, string totaltime, int rating);

        ObservableCollection<Tourlog> GetTourlogsByIdFromDB(int tourid); 

    }
}