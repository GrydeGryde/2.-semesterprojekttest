using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using _2._semesterprojekttest.Models;
using _2._semesterprojekttest.Interfaces;
using Microsoft.AspNetCore.Http;

namespace _2._semesterprojekttest.Services
{
    /// <inheritdoc />
    public class CouponService : ICouponService
    {

        private const string ConnectionString =
    "Data Source=alex-gryden-db.database.windows.net;Initial Catalog=\"Gryden DB\";Persist Security Info=True;User ID=adminlogin;Password=secret1!";

        private const string GetUserCouponsSQL = "Select * From Coupon Where UserID = @id";
        private const string CreateCouponSQL = "insert into Coupon(UserId, Info, Barcode) values (@UID, @I, @B)";
        private const string CouponCounterSQL = "Update Driver Set CouponCount = @CC where UserID = @UID";
        private const string ReadCouponCountSQL = "Select CouponCount From Driver where UserID = @UID";

        /// <inheritdoc />
        public List<Coupon> GetUserCoupons(int id)
        {
            List<Coupon> coupons = new List<Coupon>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand sql = new SqlCommand(GetUserCouponsSQL, conn))
                {
                    sql.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = sql.ExecuteReader();
                    while (reader.Read())
                    {
                        Coupon c = new Coupon();
                        c.CouponId = reader.GetInt32(0);
                        c.UserId = reader.GetInt32(1);
                        c.Info = reader.GetString(2);
                        c.Barcode = reader.GetString(3);
                        coupons.Add(c);
                    }
                }
            }
            return coupons;
        }
        /// <inheritdoc />
        public int GetCouponCount(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand sql = new SqlCommand(ReadCouponCountSQL, connection))
                {
                    sql.Parameters.AddWithValue("@UID", id);
                    SqlDataReader reader = sql.ExecuteReader();
                    if (reader.Read())
                    {
                        int count = Convert.ToInt32(reader["CouponCount"]);
                        return count;
                    }
                }
            }
            return 0;
        }
        /// <inheritdoc />
        public bool AddCouponCount(int id, int counter)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand sql = new SqlCommand(CouponCounterSQL, connection))
                {
                    sql.Parameters.AddWithValue("@UID", id);
                    sql.Parameters.AddWithValue("@CC", counter);
                    int rows = sql.ExecuteNonQuery();

                    if (rows == 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <inheritdoc />
        public bool CreateCoupon(Coupon aCoupon)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand sql = new SqlCommand(CreateCouponSQL, connection))
                {
                    sql.Parameters.AddWithValue("@UID", aCoupon.UserId);
                    sql.Parameters.AddWithValue("@I", aCoupon.Info);        
                    sql.Parameters.AddWithValue("@B", aCoupon.Barcode);

                    int rows = sql.ExecuteNonQuery();

                    if (rows == 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
