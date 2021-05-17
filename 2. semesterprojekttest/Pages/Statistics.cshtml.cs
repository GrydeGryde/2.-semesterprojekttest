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
    public class StatisticsModel : PageModel
    {
        private IProfilePicture _iPicture;
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

        public List<CruizeUser> liste { get; set; }

        public StatisticsModel(IUserService service, IProfilePicture pictureservice)
        {
            _userService = service;
            _iPicture = pictureservice;
        }
        public IActionResult OnGet()
        {
            ProfilePicture = _iPicture.GetProfilePicture(userID);
            liste = _userService.GetAllUsers();
            if (!string.IsNullOrEmpty(Filter))
            {
                liste = _userService.FilterUsers(Filter);
            }
            return Page();
        }

    }
}
