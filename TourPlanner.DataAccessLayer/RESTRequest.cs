using System;
using System.Threading.Tasks;
using System.Net.Http;
using TourPlanner.Models;
using System.Text.Json.Nodes;
using TourPlanner.Logging;
using System.Windows;
using Microsoft.Extensions.Configuration;

namespace TourPlanner.DataAccessLayer 
{
    public class RESTRequest
    {
        private HttpClient client = new HttpClient();

        private static IConfiguration config = Configuration.GetInstance();

        private string key = config["mapquest:key"];

        private static ILoggerWrapper logger = LoggerFactory.GetLogger();

        public async Task<Tour> DirectionsRequest(Tour tour)
        {
            logger.Fatal("Map created!");

            string directionsrequest = string.Empty; 
            try
            {
                directionsrequest = await client.GetStringAsync($"http://www.mapquestapi.com/directions/v2/route?key={key}&from={tour.From}&to={tour.To}&unit=k"); //maprequest
            } catch (System.Net.Http.HttpRequestException ex)
            {
                LoggerFactory.GetLogger().Error("Error while calling Mapquest API - No internet connection");
                MessageBox.Show(ex.Message + " - Make sure to be connected to the internet!", "Mapquest request failed", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }

            dynamic? jsoncontent = JsonNode.Parse(directionsrequest);

            var status = jsoncontent["info"]["statuscode"].ToString();

            if(jsoncontent["info"]["statuscode"].ToString() != "0" || jsoncontent["route"]["distance"].ToString() == "0") //if statuscode != 0 or distance == 0 --> request failed
            {
                logger.Error("HTTP Error [" + jsoncontent["info"]["statuscode"].ToString() + "] at Mapquest Request - User entered invalid location(s)");
                return null;
            }
            tour.TourDistance = jsoncontent["route"]["distance"].ToString();
            tour.EstimatedTime = jsoncontent["route"]["formattedTime"].ToString();
            return tour;
        }
        public async Task<byte[]> StaticmapRequest(Tour tour)
        {
            return await client.GetByteArrayAsync("https://open.mapquestapi.com/staticmap/v5/map?" + $"defaultMarker=none&key={key}&start={tour.From}&end={tour.To}");
        }
    }
}
