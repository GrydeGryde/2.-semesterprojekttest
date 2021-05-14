using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2._semesterprojekttest.Models;

namespace _2._semesterprojekttest.Interfaces
{
    public interface ICouponService
    {
        List<Coupon> GetUserCoupons(int id);
    }
}
