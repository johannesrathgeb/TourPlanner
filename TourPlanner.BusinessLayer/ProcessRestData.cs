using System.IO;
using System.Threading.Tasks;
using TourPlanner.DataAccessLayer;
using TourPlanner.Models; 

namespace TourPlanner.BusinessLayer
{
    public class ProcessRestData
    {
        private RESTRequest restrequest = new RESTRequest(); 

        public async Task<Tour> HandleMapRequest(Tour tour, int mode)
        {
            tour = await restrequest.DirectionsRequest(tour); //get distance and time for tour

            if(tour == null) return null; //aborts function if request fails

            if(mode == 0) //0 for create tour, 1 for edit tour
            {
                tour = TourFactory.GetInstance().AddTourToDB(tour.Name, tour.TourDescription, tour.From, tour.To, tour.TransportType, tour.TourDistance, tour.EstimatedTime);
            } else
            {
                tour = TourFactory.GetInstance().UpdateTourInDB(tour);
            }

            var staticmaprequest = await restrequest.StaticmapRequest(tour);

            File.WriteAllBytesAsync(Path.GetFullPath($"../../../../img/img{tour.Id}.png"), staticmaprequest);

            return tour; 
        }
    }
}
