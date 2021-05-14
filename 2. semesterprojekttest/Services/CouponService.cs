using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using _2._semesterprojekttest.Models;
using _2._semesterprojekttest.Interfaces;

namespace _2._semesterprojekttest.Services
{
    public class CouponService : ICouponService
    {
        private const string ConnectionString =
    "Data Source=alex-gryden-db.database.windows.net;Initial Catalog=\"Gryden DB\";Persist Security Info=True;User ID=adminlogin;Password=secret1!";

        private const string GetUserCouponsSQL = "Select * From Coupon Where UserID = @id";

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
    }
}
