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
    public class ProfilePageModel : PageModel
    {
        private IProfilePicture _iPicture;
        private IUserService _userService;
        public Picture ProfilePicture { get; set; }
        public Picture CarPicture { get; set; }
        public CruizeUser User { get; set; }
        public Driver CruizeDriver { get; set; }
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

        
        public ProfilePageModel(IProfilePicture pictureservice, IUserService userservice)
        {
            _iPicture = pictureservice;
            _userService = userservice;
        }
        public void OnGet()
        {
            ProfilePicture = _iPicture.GetProfilePicture(userID);
            CarPicture = _iPicture.GetCarPicture(userID);
            User = _userService.GetOneUser(userID);
            CruizeDriver = _userService.GetOneDriver(userID);
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("LoginLogoutUser");
        }
        public IActionResult OnPostRemoveDriver()
        {
            ProfilePicture = _iPicture.GetProfilePicture(userID);
            CarPicture = _iPicture.GetCarPicture(userID);
            User = _userService.GetOneUser(userID);
            _userService.DeleteDriver(userID);
            HttpContext.Session.SetInt32("Driver", 0);
            return Page();
        }

        public IActionResult OnPostAddDriver()
        {
            ProfilePicture = _iPicture.GetProfilePicture(userID);
            CarPicture = _iPicture.GetCarPicture(userID);
            User = _userService.GetOneUser(userID);
            _userService.AddDriver(LoggedInUser);
            CruizeDriver = _userService.GetOneDriver(userID);
            HttpContext.Session.SetInt32("Driver", 1);
            return Page();
        }

        public CruizeUser LoggedInUser
        {
            get
            {
                if (userID != 0)
                {
                    return _userService.GetOneUser(userID);
                }

                return new CruizeUser();
            }
        }
    }
}
