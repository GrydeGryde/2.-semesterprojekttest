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
    public class CouponService : ICouponService
    {
        public int validUser
        {
            get { return Convert.ToInt32(HttpContext.Session.GetInt32("Login")); }
        }
        public int userID
        {
            get { return Convert.ToInt32(HttpContext.Session.GetInt32("UserID")); }
        }
        public int validDriver
        {
            get { return Convert.ToInt32(HttpContext.Session.GetInt32("Driver")); }
        }
        public int adminLogin
        {
            get { return Convert.ToInt32(HttpContext.Session.GetInt32("Admin")); }
        }


        private const string ConnectionString =
    "Data Source=alex-gryden-db.database.windows.net;Initial Catalog=\"Gryden DB\";Persist Security Info=True;User ID=adminlogin;Password=secret1!";

        private const string GetUserCouponsSQL = "Select * From Coupon Where UserID = @id";
        private const string CreateCouponSQL = "insert into Coupon(CouponId, UserId, Info, Barcode) values (@CID, @UID, @I, @B)";
        private const string CouponCounterSQL = "Update Driver Set CouponCount = @CC where UserID = @UID";

        // This method fetches the current users available coupons //
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

        // This method creates a coupon //
        public bool CreateCoupon(Coupon aCoupon)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand sql = new SqlCommand(CreateCouponSQL, connection))
                {
                    sql.Parameters.AddWithValue("@CID", aCoupon.CouponId);
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

        // This method counts completed fares and triggers a coupon-creation when completed fares reaches 10. //
        
        int CompletedFares = 0;
        public void CompletedFare()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand sql = new SqlCommand(CouponCounterSQL, connection))
                {
                    sql.Parameters.AddWithValue("@CC", "@CC"+1);
                    sql.Parameters.AddWithValue("@UID", UserId);
                }
            }

                CompletedFares = CompletedFares++;

            if (CompletedFares == 10)
            {
                // > Trigger Create Coupon Here < //
                CompletedFares = CompletedFares - 10;
            }
       }
    }
}
