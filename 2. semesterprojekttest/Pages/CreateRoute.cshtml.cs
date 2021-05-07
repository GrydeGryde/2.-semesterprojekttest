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
        public int userID
        {
            get { return Convert.ToInt32(HttpContext.Session.GetInt32("UserID")); }
        }
        private IRouteService routeService;

        public CreateRouteModel(IRouteService service)
        {
            routeService = service;
        }
        public void OnGet()
        {
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


                routeService.AddRoute(route);
            }
        }
    }
}
