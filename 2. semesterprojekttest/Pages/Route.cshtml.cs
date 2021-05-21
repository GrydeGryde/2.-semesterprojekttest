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
        public string ErrorMessage { get; set; }
        public bool RequestCheck { get; set; }

        public bool PassengerCheck
        {
            get
            {
                foreach (CruizeUser passenger in Passengers)
                {
                    if (passenger.UserId == userID)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public List<CruizeUser> Passengers { get; set; }
        public RouteModel(IRouteService service, IProfilePicture pictureservice, IUserService userService)
        {
            _routeService = service;
            _iPicture = pictureservice;
            _userService = userService;
        }
        public int OccupiedSpace(int routeid)
        {
            return _routeService.GetAllPassengerUsers(routeid).Count;
        }
        public Picture GetPicture(int id)
        {
            return _iPicture.GetProfilePicture(id);
        }
        public CruizeUser GetDriverName(int id)
        {
            return _userService.GetOneUser(id);
        }
        public CruizeUser GetDriverAddress(int id)
        {
            return _userService.GetOneUser(id);
        }
        public string GetDay(int day)
        {
            return _routeService.GetDay(day);
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
            try
            {
                Request request = new Request();
                request.UserId = userID;
                request.RouteId = RouteID;
                request.Message = Request.Form["RequestMessage"];
                _routeService.AddRequest(request);
                RequestCheck = _routeService.CheckRequest(userID, RouteID);
                RouteProperty = _routeService.GetOneRoute(RouteID);
                ProfilePicture = _iPicture.GetProfilePicture(userID);
                SuccesApply = "You have succesfully applied to this route";
                return Page();
            }
            catch (Exception e)
            {
                ErrorMessage = "An error occoured, you probably wrote over the 500 character limit";
                ProfilePicture = _iPicture.GetProfilePicture(userID);
                RouteProperty = _routeService.GetOneRoute(RouteID);
                RequestCheck = _routeService.CheckRequest(userID, RouteID);
                Passengers = _routeService.GetAllPassengerUsers(RouteID);
                return Page();
            }
        }

        public void OnPostRemovePassenger(int id, int routeid)
        {
            _routeService.RemovePassengerUser(id, routeid);
            ProfilePicture = _iPicture.GetProfilePicture(userID);
            RouteProperty = _routeService.GetOneRoute(routeid);
            Passengers = _routeService.GetAllPassengerUsers(routeid);
            RequestCheck = _routeService.CheckRequest(userID, routeid);
        }

        public IActionResult OnPostRemoveSelf(int id, int routeid)
        {
            _routeService.RemovePassengerUser(id, routeid);
            return RedirectToPage("MyRoutes");
        }
    }
}
