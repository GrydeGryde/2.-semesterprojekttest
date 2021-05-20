using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using _2._semesterprojekttest.Models;
using _2._semesterprojekttest.Services;
using _2._semesterprojekttest.Interfaces;
using Microsoft.AspNetCore.Http;

namespace _2._semesterprojekttest.Pages
{
    public class AvailableRoutesModel : PageModel
    {
        private IProfilePicture _iPicture;
        private IRouteService _routeService;
        private IUserService _userService;
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

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }
        public List<Route> liste { get; set; }        

        public AvailableRoutesModel(IRouteService routeService, IProfilePicture pictureservice, IUserService userService)
        {
            _routeService = routeService;
            _iPicture = pictureservice;
            _userService = userService;
        }
        public CruizeUser GetDriverAddress(int id)
        {
            return _userService.GetOneUser(id);
        }
        public int OccupiedSpace(int routeid)
        {
            return _routeService.GetAllPassengerUsers(routeid).Count;
        }

        public IActionResult OnGet()
        public string GetDay(int day)
        {
            return _routeService.GetDay(day);
        }
        {
            ProfilePicture = _iPicture.GetProfilePicture(userID);
            liste = _routeService.GetAllRoutes();

            if (!string.IsNullOrEmpty(Filter))
            {
                liste = _routeService.FilterRoutes(Filter);
            }
            return Page();
        }

    }
}
