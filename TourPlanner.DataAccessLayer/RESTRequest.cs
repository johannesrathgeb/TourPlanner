using System;
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
        private string sessionId = "";
        private float ullng;
        private float ullat;
        private float lrlng;
        private float lrlat;
        private string coords = string.Empty;

        private string? time;
        public string? Time
        {
            get { return time; }
            set { time = value; }
        }

        private string? distance;

        public string? Distance
        {
            get { return distance; }
            set { distance = value; }
        }


        public async Task<bool> DirectionsRequest(string from, string to)
        {

            string directionsrequest = await client.GetStringAsync($"http://www.mapquestapi.com/directions/v2/route?key={key}&from={from}&to={to},%20Austria");

            dynamic? jsoncontent = JsonNode.Parse(directionsrequest);

            sessionId = (string)jsoncontent["route"]["sessionId"];
            ullng = (float)jsoncontent["route"]["boundingBox"]["ul"]["lng"];
            ullat = (float)jsoncontent["route"]["boundingBox"]["ul"]["lat"];
            lrlng = (float)jsoncontent["route"]["boundingBox"]["lr"]["lng"];
            lrlat = (float)jsoncontent["route"]["boundingBox"]["lr"]["lat"];


            Distance = jsoncontent["route"]["distance"].ToString();
            Time = jsoncontent["route"]["formattedTime"].ToString();

            coords = ullat.ToString(new CultureInfo("en-US")) + "," + ullng.ToString(new CultureInfo("en-US")) + "," + lrlat.ToString(new CultureInfo("en-US")) + "," + lrlng.ToString(new CultureInfo("en-US"));

            return true; 
        }

        public async Task<bool> StaticmapRequest(int tourid)
        {

            var staticmaprequest = await client.GetByteArrayAsync("https://www.mapquestapi.com/staticmap/v5/map?key=qjBgVzUoCbh1zGNWdNnowKkanIK9cADy&size=640,480&defaultMarker=none&zoom=11&rand=737758036&session=" + sessionId + "&boundingBox=" + coords);

            string imagepath = $"C:\\Users\\flole\\Documents\\FH\\4. Semester\\SWEN2\\TourplannerProj\\Tourplanner\\img\\img{tourid}.png";
            //File.WriteAllBytesAsync(imagepath, staticmaprequest);

            File.WriteAllBytesAsync(Path.GetFullPath($"../../../../img/img{tourid}.png"), staticmaprequest);

            string x = Directory.GetCurrentDirectory();

            return true; 
        }

    }
}
