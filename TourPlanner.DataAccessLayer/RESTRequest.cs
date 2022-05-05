﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Globalization;
using TourPlanner.Models;
using System.Text.Json.Nodes; 

namespace TourPlanner.DataAccessLayer
{
    public class RESTRequest
    {
        private HttpClient client = new HttpClient();

        private string key = "qjBgVzUoCbh1zGNWdNnowKkanIK9cADy";

        public async Task<Tour> DirectionsRequest(Tour tour)
        {
            string directionsrequest = await client.GetStringAsync($"http://www.mapquestapi.com/directions/v2/route?key={key}&from={tour.From}&to={tour.To}&unit=k");

            dynamic? jsoncontent = JsonNode.Parse(directionsrequest);

            var status = jsoncontent["info"]["statuscode"].ToString();
            if(jsoncontent["info"]["statuscode"].ToString() != "0" || jsoncontent["route"]["distance"].ToString() == "0")
            {
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
