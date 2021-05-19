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
    public class MyRoutesModel : PageModel
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



        public List<Route> liste { get; set; }
        public List<Route> Passengerliste { get; set; }

        public MyRoutesModel(IRouteService routeService, IProfilePicture pictureservice)
        {
            _routeService = routeService;
            _iPicture = pictureservice;
        }
        public int OccupiedSpace(int routeid)
        {
            return _routeService.GetAllPassengerUsers(routeid).Count;
        }
        public void OnGet()
        {
            ProfilePicture = _iPicture.GetProfilePicture(userID);
            Passengerliste = _routeService.GetAllPassengerRoutes(userID);
            liste = _routeService.GetAllDriverRoutes(userID);
        }
    }
}
