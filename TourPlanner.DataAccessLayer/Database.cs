using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Npgsql;
using TourPlanner.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace TourPlanner.DataAccessLayer
{
    public class Database
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .Build();


        string connectionstring;
        private NpgsqlConnection connection;

        static Database instance = new Database();
        Database()
        {
            connectionstring = $"Host={config["database:host"]};Username={config["database:username"]};Password={config["database:password"]};Database={config["database:dbname"]}";
            //MessageBox.Show($"Host={config["database:host"]};Username={config["database:username"]};Password=;Database={config["database:dbname"]}", "POG", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        public static Database getInstance()
        {
            return instance;
        }

        public NpgsqlConnection connect()
        {
            connection = new NpgsqlConnection(connectionstring);

            try
            {
            connection.Open();
            } catch (System.Net.Sockets.SocketException ex)
            {
                MessageBox.Show(ex.Message, "Invalid Database credentials", MessageBoxButton.OK, MessageBoxImage.Error);
            }


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

        public void AddTourlog(int tourid, string date, string comment, int difficulty, string totaltime, int rating)
        {
            connect();
            using (var cmd = new NpgsqlCommand("INSERT INTO tourlog (tourid, date, comment, difficulty, totaltime, rating) VALUES (@tourid, @date, @comment, @difficulty, @totaltime, @rating)", connection))
            {
                cmd.Parameters.AddWithValue("tourid", tourid);
                cmd.Parameters.AddWithValue("date", date);
                cmd.Parameters.AddWithValue("comment", comment);
                cmd.Parameters.AddWithValue("difficulty", difficulty);
                cmd.Parameters.AddWithValue("totaltime", totaltime);
                cmd.Parameters.AddWithValue("rating", rating);

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

        public ObservableCollection<Tourlog> GetTourlogsByTourId(int tourid)
        {
            connect();
            using (var cmd = new NpgsqlCommand("SELECT * FROM tourlog WHERE tourid = @tourid", connection))
            {
                cmd.Parameters.AddWithValue("tourid", tourid);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                ObservableCollection<Tourlog> loglist = new ObservableCollection<Tourlog>(); 
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        Tourlog tourlog = new Tourlog((int)reader["logid"], (string)reader["date"], (string)reader["comment"], (int)reader["difficulty"], (string)reader["totaltime"], (int)reader["rating"]);
                        loglist.Add(tourlog);
                    }
                }
                disconnect();
                return loglist;
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

        public Tourlog GetNewestTourlog()
        {
            connect();
            using (var cmd = new NpgsqlCommand("SELECT * FROM tourlog ORDER BY logid DESC LIMIT 1", connection))
            {
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                }
                Tourlog tourlog = new Tourlog((int)reader["logid"], (string)reader["date"], (string)reader["comment"], (int)reader["difficulty"], (string)reader["totaltime"], (int)reader["rating"]);

                disconnect();
                return tourlog;
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

        public void DeleteTourlog(int id)
        {
            connect();
            using (var cmd = new NpgsqlCommand("DELETE FROM tourlog WHERE logid = @id", connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
            }
            disconnect();
        }

        public void UpdateTour(Tour tour)
        {
            connect();
            using (var cmd = new NpgsqlCommand("UPDATE tour SET name = @name, tourdescription = @desc, tourfrom = @from, tourto = @to, transporttype = @type, tourdistance = @dist, estimatedtime = @est WHERE id = @id", connection))
            {
                cmd.Parameters.AddWithValue("id", tour.Id);
                cmd.Parameters.AddWithValue("name", tour.Name);
                cmd.Parameters.AddWithValue("desc", tour.TourDescription);
                cmd.Parameters.AddWithValue("from", tour.From);
                cmd.Parameters.AddWithValue("to", tour.To);
                cmd.Parameters.AddWithValue("type", (int)tour.TransportType);
                cmd.Parameters.AddWithValue("dist", tour.TourDistance);
                cmd.Parameters.AddWithValue("est", tour.EstimatedTime);
                NpgsqlDataReader reader = cmd.ExecuteReader();
            }
            disconnect();
        }

        public void UpdateTourlog(Tourlog tourlog)
        {
            connect();
            using (var cmd = new NpgsqlCommand("UPDATE tourlog SET date = @date, comment = @comment, difficulty = @difficulty, totaltime = @totaltime, rating = @rating WHERE logid = @logid", connection))
            {
                cmd.Parameters.AddWithValue("logid", tourlog.Id);
                cmd.Parameters.AddWithValue("date", tourlog.Date);
                cmd.Parameters.AddWithValue("comment", tourlog.Comment);
                cmd.Parameters.AddWithValue("difficulty", tourlog.Difficulty);
                cmd.Parameters.AddWithValue("totaltime", tourlog.Totaltime);
                cmd.Parameters.AddWithValue("rating", tourlog.Rating);

                NpgsqlDataReader reader = cmd.ExecuteReader();
            }
            disconnect();
        }

        public Tour GetTourById(int id)
        {

            connect();
            using (var cmd = new NpgsqlCommand("SELECT * FROM tour WHERE id = @id", connection))
            {
                cmd.Parameters.AddWithValue("id", id);
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

        public Tourlog GetTourlogByLogId(int logid)
        {

            connect();
            using (var cmd = new NpgsqlCommand("SELECT * FROM tourlog WHERE logid = @logid", connection))
            {
                cmd.Parameters.AddWithValue("logid", logid);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                }
                Tourlog tourlog = new Tourlog((int)reader["logid"], (string)reader["date"], (string)reader["comment"], (int)reader["difficulty"], (string)reader["totaltime"], (int)reader["rating"]);
                disconnect();
                return tourlog;
            }
        }

        public List<Tour> SearchText(string itemName)
        {
            connect();
            using (var cmd = new NpgsqlCommand("SELECT DISTINCT tour.* FROM tour LEFT JOIN tourlog ON (tour.id = tourlog.logid) WHERE LOWER(name) LIKE @itemname OR LOWER(tourdescription) LIKE @itemname OR LOWER(tourfrom) LIKE @itemname OR LOWER(tourto) LIKE @itemname OR LOWER(tourlog.comment) LIKE @itemname", connection))
            {
                cmd.Parameters.AddWithValue("itemname", "%" + itemName + "%");
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

    }
}
