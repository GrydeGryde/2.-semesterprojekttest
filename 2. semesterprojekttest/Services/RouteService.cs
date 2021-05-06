using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using _2._semesterprojekttest.Interfaces;
using _2._semesterprojekttest.Models;

namespace _2._semesterprojekttest.Services
{
    public class RouteService : IRouteService
    {
        private const string ConnectionString =
            "Data Source=alex-gryden-db.database.windows.net;Initial Catalog=\"Gryden DB\";Persist Security Info=True;User ID=adminlogin;Password=secret1!";
        public bool AddRoute(Route route)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand sql = new SqlCommand("Insert into Route(UserID, Start, Goal, Day, Arrival, Space) values (@UID, @S, @G, @D, @Arr, @Spa)", connection))
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

        public bool RemoveRoute(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRoute(int id, Route route)
        {
            throw new NotImplementedException();
        }
    }
}
