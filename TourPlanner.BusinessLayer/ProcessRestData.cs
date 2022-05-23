using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BusinessLayer.Logging;
using TourPlanner.DataAccessLayer;
using TourPlanner.Models; 

namespace TourPlanner.BusinessLayer
{
    public class ProcessRestData
    {

        private RESTRequest restrequest = new RESTRequest(); 


        public async Task<Tour> HandleMapRequest(Tour tour, int mode)
        {
            //((IDataAccessDependency)restrequest).SetDependency((log4net.Core.ILoggerWrapper)LoggerFactory.GetLogger());

            tour = await restrequest.DirectionsRequest(tour);

            if(tour == null) return null;

            if(mode == 0)
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
