using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using _2._semesterprojekttest.Models;
using _2._semesterprojekttest.Services;
using _2._semesterprojekttest.Interfaces;

namespace _2._semesterprojekttest.Pages
{
    public class AvailableRoutesModel : PageModel
    {
        private IRouteService _routeService;

        public List<Route> liste { get; set; }

        public AvailableRoutesModel(IRouteService routeService)
        {
            _routeService = routeService;
        }
        public void OnGet()
        {
            liste = _routeService.GetAllRoutes();
        }
    }
}
