using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using _2._semesterprojekttest.Services;
using _2._semesterprojekttest.Interfaces;
using _2._semesterprojekttest.Models;
using Microsoft.AspNetCore.Http;

namespace _2._semesterprojekttest.Pages
{
    public class DriverRequestsModel : PageModel
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

        public string SuccesAccept { get; set; }
        public string SuccesDecline { get; set; }

        public List<Request> liste { get; set; }
        
        public DriverRequestsModel(IRouteService routeService, IProfilePicture pictureservice)
        {
            _routeService = routeService;
            _iPicture = pictureservice;
        }
        public void OnGet()
        {
            ProfilePicture = _iPicture.GetProfilePicture(userID);
            liste = _routeService.GetAllRequests(userID);
        }

        public IActionResult OnPostAccept(int UserID, int RouteID, int RequestID)
        {
            SuccesAccept = "The request has been accepted.";
            _routeService.AcceptRequest(UserID, RouteID);
            _routeService.DeleteRequest(RequestID);
            _routeService.ReduceSpace(RouteID);
            liste = _routeService.GetAllRequests(userID);
            return Page();
        }
        public IActionResult OnPostDecline(int RequestID)
        {
            SuccesDecline = "The request has been declined.";
            _routeService.DeleteRequest(RequestID);
            liste = _routeService.GetAllRequests(userID);
            return Page();
        }
    }
}
