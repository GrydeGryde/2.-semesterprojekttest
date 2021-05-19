using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using _2._semesterprojekttest.Models;
using _2._semesterprojekttest.Services;
using _2._semesterprojekttest.Interfaces;

namespace _2._semesterprojekttest.Pages
{
    public class RouteModel : PageModel
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

        public List<CruizeUser> liste { get; set; }

        private IProfilePicture _iPicture;

        private IUserService _userService;

        public Picture ProfilePicture { get; set; }

        private IRouteService _routeService;
        public Route RouteProperty { get; set; }
        public string SuccesApply { get; set; }

        public bool RequestCheck { get; set; }
        public List<CruizeUser> Passengers { get; set; }
        public RouteModel(IRouteService service, IProfilePicture pictureservice, IUserService userService)
        {
            _routeService = service;
            _iPicture = pictureservice;
            _userService = userService;
        }
        public void OnGet(int id)
        {
            ProfilePicture = _iPicture.GetProfilePicture(userID);
            RouteProperty = _routeService.GetOneRoute(id);
            RequestCheck = _routeService.CheckRequest(userID, id);
            Passengers = _routeService.GetAllPassengerUsers(id);
        }

        public IActionResult OnPost(int UserID, int RouteID)
        {
            SuccesApply = "You have succesfully applied to this route";
            Request request = new Request();
            request.UserId = userID;
            request.RouteId = RouteID;
            request.Message = Request.Form["RequestMessage"];
            _routeService.AddRequest(request);
            RequestCheck = _routeService.CheckRequest(userID, RouteID);
            RouteProperty = _routeService.GetOneRoute(RouteID);
            ProfilePicture = _iPicture.GetProfilePicture(userID);
            return Page();
        }

        public void OnPostRemovePassenger(int id, int routeid)
        {
            _routeService.RemovePassengerUser(id, routeid);
            _routeService.IncreaseSpace(routeid);
            ProfilePicture = _iPicture.GetProfilePicture(userID);
            RouteProperty = _routeService.GetOneRoute(routeid);
            Passengers = _routeService.GetAllPassengerUsers(routeid);
            RequestCheck = _routeService.CheckRequest(userID, routeid);
        }
    }
}
