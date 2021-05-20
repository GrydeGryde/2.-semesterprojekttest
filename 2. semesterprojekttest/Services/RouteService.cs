using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using _2._semesterprojekttest.Interfaces;
using _2._semesterprojekttest.Models;
using Microsoft.AspNetCore.SignalR;

namespace _2._semesterprojekttest.Services
{
    public class RouteService : IRouteService
    {
        private IUserService _userService = new UserService();
        private const string ConnectionString =
            "Data Source=alex-gryden-db.database.windows.net;Initial Catalog=\"Gryden DB\";Persist Security Info=True;User ID=adminlogin;Password=secret1!";

        private const string GetOneRouteSQL = "select * from Route where RouteID = @ID";

        public List<Route> GetAllRoutes()
        {
            List<Route> liste = new List<Route>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand sql = new SqlCommand("select * from Route", conn))
                {
                    SqlDataReader reader = sql.ExecuteReader();
                    while (reader.Read())
                    {
                        Route r = new Route();
                        r.RouteId = reader.GetInt32(0);
                        r.UserId = reader.GetInt32(1);
                        r.Start = reader.GetString(2);
                        r.Goal = reader.GetString(3);
                        r.Day = reader.GetInt32(4);
                        r.Arrival = reader.GetDateTime(5);
                        r.Space = reader.GetInt32(6);
                        liste.Add(r);
                    }
                }
            }

            return liste;
        }

        public bool AddRoute(Route route)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand sql =
                    new SqlCommand(
                        "Insert into Route(UserID, Start, Goal, Day, Arrival, Space) values (@UID, @S, @G, @D, @Arr, @Spa)",
                        connection))
                {
                    sql.Parameters.AddWithValue("@UID", route.UserId);
                    sql.Parameters.AddWithValue("@S", route.Start);
                    sql.Parameters.AddWithValue("@G", route.Goal);
                    sql.Parameters.AddWithValue("@D", route.Day);
                    sql.Parameters.AddWithValue("@Arr", route.Arrival);
                    sql.Parameters.AddWithValue("@Spa", route.Space);
                    int rows = sql.ExecuteNonQuery();

                    if (rows == 1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private Route MakeRoute(SqlDataReader reader)
        {
            Route route = new Route
            {
                RouteId = Convert.ToInt32(reader["RouteID"]),
                UserId = Convert.ToInt32(reader["UserID"]),
                Start = Convert.ToString(reader["Start"]),
                Goal = Convert.ToString(reader["Goal"]),
                Day = Convert.ToInt32(reader["Day"]),
                Arrival = Convert.ToDateTime(reader["Arrival"]),
                Space = Convert.ToInt32(reader["Space"])
            };

            return route;
        }

        public Route GetOneRoute(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand sql = new SqlCommand(GetOneRouteSQL, connection))
                {
                    sql.Parameters.AddWithValue("@ID", id);

                    SqlDataReader reader = sql.ExecuteReader();

                    if (reader.Read())
                    {
                        return MakeRoute(reader);
                    }
                }
            }

            return null;
        }

        public bool RemoveRoute(int routeid)
        {
            using SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            using (SqlCommand sql =
                new SqlCommand("DELETE * FROM Route WHERE RouteID=@RID ", conn))
            {
                sql.Parameters.AddWithValue("@RID", routeid);
                int rows = sql.ExecuteNonQuery();

                if (rows == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public bool UpdateRoute(int id, Route route)
        {
            throw new NotImplementedException();
        }

        public bool AddRequest(Request request)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand sql =
                    new SqlCommand("Insert into Request(UserID, RouteID, Message) values (@UID, @RID, @M)", connection))
                {
                    sql.Parameters.AddWithValue("@UID", request.UserId);
                    sql.Parameters.AddWithValue("@RID", request.RouteId);
                    sql.Parameters.AddWithValue("@M", request.Message);
                    int rows = sql.ExecuteNonQuery();

                    if (rows == 1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public Request DeleteRequest(int id)
        {
            Request request = GetOneRequest(id);

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand sql = new SqlCommand("delete from Request where RequestID = @ID", connection))
                {
                    sql.Parameters.AddWithValue("@ID", id);

                    int affectedRows = sql.ExecuteNonQuery();

                    if (affectedRows == 1)
                    {
                        return request;
                    }
                }
            }

            return null;
        }

        public Request GetOneRequest(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand sql = new SqlCommand("select * from Request where RequestID = @ID", connection))
                {
                    sql.Parameters.AddWithValue("@ID", id);

                    SqlDataReader reader = sql.ExecuteReader();

                    if (reader.Read())
                    {
                        return MakeRequest(reader);
                    }
                }
            }

            return null;
        }

        private Request MakeRequest(SqlDataReader reader)
        {
            Request request = new Request
            {
                RequestId = Convert.ToInt32(reader["RequestID"]),
                UserId = Convert.ToInt32(reader["UserID"]),
                RouteId = Convert.ToInt32(reader["RouteID"]),
                Message = Convert.ToString(reader["Message"]),
            };

            return request;
        }

        public List<Request> GetAllRequests(int id)
        {
            List<Request> liste = new List<Request>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand sql = new SqlCommand("select * from DriversRequestsView where DriverID = @ID", conn))
                {

                    sql.Parameters.AddWithValue("@ID", id);
                    SqlDataReader reader = sql.ExecuteReader();
                    while (reader.Read())
                    {
                        Request r = new Request();
                        r.RequestId = reader.GetInt32(0);
                        r.RouteId = reader.GetInt32(1);
                        r.UserId = reader.GetInt32(2);
                        r.Message = reader.GetString(3);
                        liste.Add(r);
                    }
                }
            }

            return liste;
        }

        public bool AcceptRequest(int UserID, int RouteID)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand sql = new SqlCommand("Insert into Passenger(UserID, RouteID) values (@UID, @RID)",
                    connection))
                {
                    sql.Parameters.AddWithValue("@UID", UserID);
                    sql.Parameters.AddWithValue("@RID", RouteID);
                    int rows = sql.ExecuteNonQuery();

                    if (rows == 1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool ReduceSpace(int RouteID)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand sql = new SqlCommand("UPDATE Route SET Space = Space - 1 where RouteID = @RID",
                    connection))
                {
                    sql.Parameters.AddWithValue("@RID", RouteID);
                    int rows = sql.ExecuteNonQuery();

                    if (rows == 1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IncreaseSpace(int RouteID)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand sql = new SqlCommand("UPDATE Route SET Space = Space + 1 where RouteID = @RID",
                    connection))
                {
                    sql.Parameters.AddWithValue("@RID", RouteID);
                    int rows = sql.ExecuteNonQuery();

                    if (rows == 1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public List<Route> GetAllPassengerRoutes(int id)
        {
            List<Passenger> pliste = new List<Passenger>();
            List<Route> liste = new List<Route>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand sql = new SqlCommand("SELECT * FROM Passenger WHERE UserID = @ID", conn))
                {

                    sql.Parameters.AddWithValue("@ID", id);
                    SqlDataReader reader = sql.ExecuteReader();
                    while (reader.Read())
                    {
                        Passenger p = new Passenger();
                        p.RouteId = reader.GetInt32(1);
                        pliste.Add(p);
                    }

                    foreach (Passenger passenger in pliste)
                    {
                        liste.Add(GetOneRoute(passenger.RouteId));
                    }
                }
            }

            return liste;
        }

        public List<Route> GetAllDriverRoutes(int id)
        {
            List<Route> liste = new List<Route>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand sql = new SqlCommand("SELECT * FROM Route WHERE UserID = @ID", conn))
                {

                    sql.Parameters.AddWithValue("@ID", id);
                    SqlDataReader reader = sql.ExecuteReader();
                    while (reader.Read())
                    {
                        Route r = new Route();
                        r.RouteId = Convert.ToInt32(reader["RouteID"]);
                        r.Day = Convert.ToInt32(reader["Day"]);
                        r.Arrival = Convert.ToDateTime(reader["Arrival"]);
                        r.Goal = Convert.ToString(reader["Goal"]);
                        r.Start = Convert.ToString(reader["Start"]);
                        r.Space = Convert.ToInt32(reader["Space"]);
                        liste.Add(r);
                    }
                }
            }

            return liste;
        }

        public bool CheckRequest(int UserID, int RouteID)
        {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand sql =
                        new SqlCommand("select * from Request where (UserID = @UID) AND (RouteID = @RID)", connection))
                    {
                        sql.Parameters.AddWithValue("UID", UserID);
                        sql.Parameters.AddWithValue("RID", RouteID);

                        SqlDataReader reader = sql.ExecuteReader();

                        if (reader.Read())
                        {
                            return true;
                        }
                    }
                }

                return false;
        }
        public List<CruizeUser> GetAllPassengerUsers(int routeid)
        {
            List<CruizeUser> pliste = new List<CruizeUser>();
            

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand sql = new SqlCommand("SELECT * FROM Passenger WHERE RouteID = @RID", conn))
                {

                    sql.Parameters.AddWithValue("@RID", routeid);
                    SqlDataReader reader = sql.ExecuteReader();
                    while (reader.Read())
                    {
                        int userid = Convert.ToInt32(reader["UserID"]);
                        CruizeUser p = _userService.GetOneUser(userid);
                        pliste.Add(p);
                    }
                    
                }
            }
            return pliste;
        }

        public bool RemovePassengerUser(int userid, int routeid)
        {
            using SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            using (SqlCommand sql =
                new SqlCommand("Delete FROM Passenger WHERE (UserID = @UID) AND (RouteID=@RID)", conn))
            {
                sql.Parameters.AddWithValue("@RID", routeid);
                sql.Parameters.AddWithValue("@UID", userid);
                int rows = sql.ExecuteNonQuery();

                if (rows == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public string GetDay(int day)
        {
            switch (day)
            {
                case 1:
                    return "Monday";
                case 2:
                    return "Tuesday";
                case 3:
                    return "Wednesday";
                case 4:
                    return "Thursday";
                case 5:
                    return "Friday";
                case 6:
                    return "Saturday";
                case 7:
                    return "Sunday";
            }

            return null;
        }
    }
}

