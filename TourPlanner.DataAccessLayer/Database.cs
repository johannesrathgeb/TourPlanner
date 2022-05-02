using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using TourPlanner.Models;   

namespace TourPlanner.DataAccessLayer
{
    public class Database
    {
        const string connectionstring = "Host=localhost;Username=postgres;Password=;Database=Tourplanner";
        private NpgsqlConnection connection;

        static Database instance = new Database();
        Database()
        {

        }

        public static Database getInstance()
        {
            return instance;
        }

        public NpgsqlConnection connect()
        {
            connection = new NpgsqlConnection(connectionstring);
            connection.Open();
            return connection;
        }

        public void disconnect()
        {
            connection.Close();
        }

        public void AddTour(string name, string tourdescription, string from, string to, TransportType transporttype, string tourdistance, string estimatedtime)
        {
            connect();
            using (var cmd = new NpgsqlCommand("INSERT INTO tour (name, tourdescription, tourfrom, tourto, transporttype, tourdistance, estimatedtime) VALUES (@name, @desc, @from, @to, @type, @dist, @est)", connection))
            {
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("desc", tourdescription);
                cmd.Parameters.AddWithValue("from", from);
                cmd.Parameters.AddWithValue("to", to);
                cmd.Parameters.AddWithValue("type", (int)transporttype);
                cmd.Parameters.AddWithValue("dist", tourdistance);
                cmd.Parameters.AddWithValue("est", estimatedtime);
                cmd.ExecuteNonQuery();
            }
            disconnect();
        }
        
        public List<Tour> GetAllTours()
        {
            connect();
            using (var cmd = new NpgsqlCommand("SELECT * FROM tour", connection))
            {
                NpgsqlDataReader reader = cmd.ExecuteReader();
                List<Tour> tourlist = new List<Tour>();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        Tour tour = new Tour((int)reader["id"], (string)reader["name"], (string)reader["tourdescription"], (string)reader["tourfrom"], (string)reader["tourto"], (TransportType)reader["transporttype"], (string)reader["tourdistance"], (string)reader["estimatedtime"]);
                        tourlist.Add(tour);
                    }
                }
                disconnect();
                return tourlist;
            }
        }

        public Tour GetNewestTour()
        {
            connect();
            using (var cmd = new NpgsqlCommand("SELECT * FROM tour ORDER BY id DESC LIMIT 1", connection))
            {
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                }
                Tour tour = new Tour((int)reader["id"], (string)reader["name"], (string)reader["tourdescription"], (string)reader["tourfrom"], (string)reader["tourto"], (TransportType)reader["transporttype"], (string)reader["tourdistance"], (string)reader["estimatedtime"]);

                disconnect();
                return tour;
            }
        }

        public void DeleteTour(int id)
        {
            connect();
            using (var cmd = new NpgsqlCommand("DELETE FROM tour WHERE id = @id", connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
            }
            disconnect();
        }





    }
}
