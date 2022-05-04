using System;
using System.Collections.Generic;
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

    }
}