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
    public class DummyReportPageModel : PageModel
    {
        public CruizeUser user;
        public List<CruizeUser> _listeUsers;
        private IUserService _userService;
        private IProfilePicture _iPicture;
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
        public Picture GetPicture(int id)
        {
            return _iPicture.GetProfilePicture(id);
        }

        public DummyReportPageModel(IUserService service, IProfilePicture pictureservice)
        {
            _userService = service;
            _iPicture = pictureservice;
        }
        public void OnGet()
        {
            ProfilePicture = _iPicture.GetProfilePicture(userID);
            _listeUsers = _userService.GetAllUsers();
        }
    }
}
