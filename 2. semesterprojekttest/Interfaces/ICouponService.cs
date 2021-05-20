using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2._semesterprojekttest.Models;

namespace _2._semesterprojekttest.Interfaces
{
    /// <summary>
    /// This interface used to access the Coupon service methods.
    /// </summary>
    public interface ICouponService
    {
        /// <summary>
        /// This method fetches the current users available coupons from the database.
        /// </summary>
        /// <param name="id">The Drivers user Id.</param>
        /// <returns>A List of all the Coupons that driver has earned.</returns>
        List<Coupon> GetUserCoupons(int id);

        /// <summary>
        /// This method creates a coupon
        /// </summary>
        /// <param name="aCoupon"></param>
        /// <returns></returns>
        bool CreateCoupon(Coupon aCoupon);

        /// <summary>
        /// This class is used to count how many coupons a specific driver has earned.
        /// </summary>
        /// <param name="id">The Drivers userid</param>
        /// <returns>A number of how many coupons</returns>
        int GetCouponCount(int id);

        /// <summary>
        /// Updates the Couponcount for by a certain amount for a driver in Driver table
        /// </summary>
        /// <param name="id">A drivers userid</param>
        /// <param name="counter">The amount you want to add to the counter</param>
        /// <returns>True if it succeed or false if it fails</returns>
        bool AddCouponCount(int id, int counter);
    }
}
