using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using _2._semesterprojekttest.Models;
using _2._semesterprojekttest.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;

namespace _2._semesterprojekttest.Pages
{
    public class CouponsModel : PageModel
    {
        private IProfilePicture _iPicture;
        private ICouponService _couponService;
        public Picture ProfilePicture { get; set; }
        public int count { get; set; }
        public int FaresLeft = 10;

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

        public List<Coupon> Coupons;
        


        public CouponsModel(ICouponService couponService, IProfilePicture pictureservice)
        {
            _couponService = couponService;
            _iPicture = pictureservice;
        }

        public void OnGet()
        {
            ProfilePicture = _iPicture.GetProfilePicture(userID);
            Coupons = _couponService.GetUserCoupons(userID);
            count = _couponService.GetCouponCount(userID);
            FaresLeft = FaresLeft - count;
        }

        public void OnPost()
        {
            Coupon aCoupon = new Coupon();
            aCoupon.UserId = userID; // Connects the created coupon to the current users ID //
            aCoupon.Info = "This coupon can be redeemed in the cafeteria for a free sandwich";
            aCoupon.Barcode = "570020010042069";

            int counter = _couponService.GetCouponCount(userID);
            if (counter < 10)
            {
                counter = counter + 1;
                _couponService.AddCouponCount(userID, counter);
            }

            if (counter == 10)
            {
                _couponService.CreateCoupon(aCoupon);
                _couponService.AddCouponCount(userID, 0);
            }

            Coupons = _couponService.GetUserCoupons(userID);
            count = counter;
            FaresLeft = FaresLeft - counter;
            ProfilePicture = _iPicture.GetProfilePicture(userID);
        }
    }
}
