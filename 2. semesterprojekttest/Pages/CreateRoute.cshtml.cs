using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2._semesterprojekttest.Interfaces;
using _2._semesterprojekttest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _2._semesterprojekttest.Pages
{
    public class CreateRouteModel : PageModel
    {
        private IProfilePicture _iPicture;
        private IRouteService _routeService;
        public Picture ProfilePicture { get; set; }
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

        public CreateRouteModel(IRouteService service, IProfilePicture pictureservice)
        {
            _routeService = service;
            _iPicture = pictureservice;
        }
        public void OnGet()
        {
            ProfilePicture = _iPicture.GetProfilePicture(userID);
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                Route route = new Route();
                route.UserId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
                route.Start = Request.Form["AddStart"];
                route.Goal = Request.Form["AddEnd"];
                route.Day = Convert.ToInt32(Request.Form["Day"]);
                route.Arrival = Convert.ToDateTime(Request.Form["Arrival"]);
                route.Space = Convert.ToInt32(Request.Form["Space"]);


                _routeService.AddRoute(route);
            }
        }
    }
}
