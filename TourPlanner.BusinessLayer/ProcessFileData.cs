using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TourPlanner.DataAccessLayer;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    public class ProcessFileData
    {

        public async Task<ObservableCollection<Tour>> ImportTours(string filepath)
        {
            ProcessRestData prd = new ProcessRestData();
            ObservableCollection<Tour> tourlist = new ObservableCollection<Tour>();

            FileAccess fileaccess = new FileAccess(); 
            string tourdata = fileaccess.GetTourData(filepath);
            dynamic? jsoncontent = JsonNode.Parse(tourdata);

            string ooo = jsoncontent[0]["Tourlogs"][0]["Date"].ToString(); 
            //string iii = jsoncontent[1]["Tourlogs"]["Date"].ToString(); 

            for(int i = 0; i < jsoncontent.Count; i++)
            {
                int tt = (int)jsoncontent[i]["TransportType"]; 
                Tour tour = new Tour(jsoncontent[i]["Name"].ToString(), jsoncontent[i]["TourDescription"].ToString(), jsoncontent[i]["From"].ToString(), jsoncontent[i]["To"].ToString(), (TransportType)tt);

                tour = await prd.HandleMapRequest(tour, 0);

                for (int j = 0; j < jsoncontent[i]["Tourlogs"].Count; j++)
                {
                    Tourlog tourlog = new Tourlog(jsoncontent[i]["Tourlogs"][j]["Date"].ToString(), jsoncontent[i]["Tourlogs"][j]["Comment"].ToString(), (int)jsoncontent[i]["Tourlogs"][j]["Difficulty"], jsoncontent[i]["Tourlogs"][j]["Totaltime"].ToString(), (int)jsoncontent[i]["Tourlogs"][j]["Rating"]);
                    tourlog = TourFactory.GetInstance().AddTourlogToDB(tour.Id ,jsoncontent[i]["Tourlogs"][j]["Date"].ToString(), jsoncontent[i]["Tourlogs"][j]["Comment"].ToString(), (int)jsoncontent[i]["Tourlogs"][j]["Difficulty"], jsoncontent[i]["Tourlogs"][j]["Totaltime"].ToString(), (int)jsoncontent[i]["Tourlogs"][j]["Rating"]);
                    tour.Tourlogs.Add(tourlog);



                }

                tourlist.Add(tour);
            }

            return tourlist; 

        }


    }
}
