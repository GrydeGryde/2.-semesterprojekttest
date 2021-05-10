using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using _2._semesterprojekttest.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using _2._semesterprojekttest.Interfaces;

namespace _2._semesterprojekttest.Services
{
    public class UserService : IUserService
    {
        private const string ConnectionString =
            "Data Source=alex-gryden-db.database.windows.net;Initial Catalog=\"Gryden DB\";Persist Security Info=True;User ID=adminlogin;Password=secret1!";

        private const string AddDriverSQL = "insert into Driver(UserID) values (@UID)";
        private const string AddUserSQL = "insert into CruizeUser(FirstName, LastName, Email, Password, Address, Zipcode) values (@FN, @LN, @E, @P, @AD, @ZC)";
        private const string DeleteUserSQL = "delete from CruizeUser where UserID = @ID";
        private const string GetAllUsersSQL = "select * from CruizeUser";
        private const string GetOneUserSQL = "select * from CruizeUser where UserID = @ID";
        private const string DeleteDriverSQL = "Delete from Driver where UserID = @ID";
        private const string CheckDriverSQL = "Select * from Driver Where UserID = @ID";

        public bool AddUser(CruizeUser user)
        {
            user.Password = PasswordHash(user.Email, user.Password);

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand sql = new SqlCommand(AddUserSQL, connection))
                {
                    sql.Parameters.AddWithValue("@FN", user.FirstName);
                    sql.Parameters.AddWithValue("@LN", user.LastName);
                    sql.Parameters.AddWithValue("@E", user.Email);
                    sql.Parameters.AddWithValue("@P", user.Password);
                    sql.Parameters.AddWithValue("@AD", user.Address);
                    sql.Parameters.AddWithValue("@ZC", user.Zipcode);

                    int rows = sql.ExecuteNonQuery();

                    if (rows == 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool AddDriver(CruizeUser user)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand sql = new SqlCommand(AddDriverSQL, connection))
                {
                    sql.Parameters.AddWithValue("@UID", GetUserId(user.Email));

                    int rows = sql.ExecuteNonQuery();

                    if (rows == 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CheckDriver(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                {
                    using (SqlCommand sql = new SqlCommand(CheckDriverSQL, connection))
                    {
                        sql.Parameters.AddWithValue("@ID", id);

                        SqlDataReader reader = sql.ExecuteReader();

                        if (reader.Read())
                        {
                            return true;
                        }
                    }
                } 
            }
            return false;
        }

        public bool DeleteDriver(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                {
                    using (SqlCommand sql = new SqlCommand(DeleteDriverSQL, connection))
                    {
                        sql.Parameters.AddWithValue("@ID", id);

                        int rows = sql.ExecuteNonQuery();

                        if (rows == 1)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public CruizeUser DeleteUser(int id)
        {
            CruizeUser user = GetOneUser(id);

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand sql = new SqlCommand(DeleteUserSQL, connection))
                {
                    sql.Parameters.AddWithValue("@ID", id);

                    int affectedRows = sql.ExecuteNonQuery();

                    if (affectedRows == 1)
                    {
                        return user;
                    }
                }
            }

            return null;
        }

        public List<CruizeUser> GetAllUsers()
        {
            List<CruizeUser> liste = new List<CruizeUser>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand sql = new SqlCommand(GetAllUsersSQL, connection))
                {
                    SqlDataReader reader = sql.ExecuteReader();

                    while (reader.Read())
                    {
                        CruizeUser user = MakeUser(reader);
                        liste.Add(user);
                    }
                }
            }

            return liste;
        }

        public CruizeUser GetOneUser(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand sql = new SqlCommand(GetOneUserSQL, connection))
                {
                    sql.Parameters.AddWithValue("@ID", id);

                    SqlDataReader reader = sql.ExecuteReader();

                    if (reader.Read())
                    {
                        return MakeUser(reader);
                    }
                }
            }

            return null;
        }
        public int GetUserId(string email)
        {
            foreach (CruizeUser user in GetAllUsers())
            {
                if (user.Email == email)
                {
                    return user.UserId;
                }
            }

            return 0;
        }

        public bool UpdateUser(int id, CruizeUser user)
        {
            throw new NotImplementedException();
        }
        private CruizeUser MakeUser(SqlDataReader reader)
        {
            CruizeUser user = new CruizeUser
            {
                UserId = Convert.ToInt32(reader["UserID"]),
                FirstName = Convert.ToString(reader["FirstName"]),
                LastName = Convert.ToString(reader["LastName"]),
                Email = Convert.ToString(reader["Email"]),
                Password = Convert.ToString(reader["Password"]),
                Address = Convert.ToString(reader["Address"])
            };

            return user;
        }

        public string PasswordHash(string userName, string password)
        {
            PasswordHasher<string> pw = new PasswordHasher<string>();
            string passwordHashed = pw.HashPassword(userName, password);
            return passwordHashed;
        }

        public bool CheckPassword(string userName, string password)
        {
            bool loggedIn = false;
            foreach (var user in GetAllUsers())
            {
                if (user.Email == userName)
                {
                    string DBpassword = user.Password;
                    PasswordHasher<string> pw = new PasswordHasher<string>();
                    var verificationResult = pw.VerifyHashedPassword(userName, DBpassword, password);
                    if (verificationResult == PasswordVerificationResult.Success)
                        loggedIn = true;
                    return loggedIn;
                }
            }

            return loggedIn;
        }


    }
}
