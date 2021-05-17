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

        public List<Coupon> Coupons;
        private ICouponService _couponService;


        public CouponsModel(ICouponService couponService)
        {
            _couponService = couponService;
        }

        public void OnGet()
        {
            Coupons = _couponService.GetUserCoupons(userID);
        }

        public void OnPost()
        {
            Coupon aCoupon = new Coupon();
            aCoupon.UserId = userID; // Connects the created coupon to the current users ID //
            aCoupon.Info = "Dette er en madkupon, indløs den i cafeteriet";
            aCoupon.Barcode = "570020010042069";

            _couponService.CreateCoupon(aCoupon);
        }
    }
}
