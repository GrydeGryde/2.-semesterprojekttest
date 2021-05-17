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

        private IProfilePicture _iPicture;

        public Picture ProfilePicture { get; set; }

        private IRouteService routeService;
        public Route RouteProperty { get; set; }
        public RouteModel(IRouteService service, IProfilePicture pictureservice)
        {
            routeService = service;
            _iPicture = pictureservice;
        }
        public void OnGet(int id)
        {
            RouteProperty = routeService.GetOneRoute(id);
        }

        public IActionResult OnPost(int UserID, int RouteID)
        {
            Request request = new Request();
            request.UserId = userID;
            request.RouteId = RouteID;
            request.Message = Request.Form["RequestMessage"];
            routeService.AddRequest(request);
            return RedirectToPage("Index");
        }
    }
}
