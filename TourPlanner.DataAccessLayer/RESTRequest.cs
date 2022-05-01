using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Globalization;

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
        


        public async void DirectionsRequest()
        {
            string from = "Wien";
            string to = "Salzburg";

            string directionsrequest = await client.GetStringAsync($"http://www.mapquestapi.com/directions/v2/route?key={key}&from={from}&to={to},%20Austria");

            dynamic? jsoncontent = JsonConvert.DeserializeObject(directionsrequest);

            sessionId = jsoncontent.route.sessionId;
            ullng = jsoncontent.route.boundingBox.ul.lng;
            ullat = jsoncontent.route.boundingBox.ul.lat;
            lrlng = jsoncontent.route.boundingBox.lr.lng;
            lrlat = jsoncontent.route.boundingBox.lr.lat;

            coords = ullat.ToString(new CultureInfo("en-US")) + "," + ullng.ToString(new CultureInfo("en-US")) + "," + lrlat.ToString(new CultureInfo("en-US")) + "," + lrlng.ToString(new CultureInfo("en-US"));


            StaticmapRequest();
        }

        public async void StaticmapRequest()
        {

            var staticmaprequest = await client.GetByteArrayAsync("https://www.mapquestapi.com/staticmap/v5/map?key=qjBgVzUoCbh1zGNWdNnowKkanIK9cADy&size=640,480&defaultMarker=none&zoom=11&rand=737758036&session=" + sessionId + "&boundingBox=" + coords);

            string imagepath = "C:\\Users\\flole\\Documents\\FH\\4. Semester\\SWEN2\\TourPlanner Github Project\\img\\img2.png";
            File.WriteAllBytesAsync(imagepath, staticmaprequest);

            string x = Directory.GetCurrentDirectory();

        }


    }
}

