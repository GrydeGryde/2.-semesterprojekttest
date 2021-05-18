using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2._semesterprojekttest.Models;
using _2._semesterprojekttest.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _2._semesterprojekttest.Pages
{
    public class LoginLogoutUser : PageModel
    {

        private IProfilePicture _iPicture;

        public Picture ProfilePicture
        {
            get; set;
        }
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

        public CruizeUser LoggedInUser
        {
            get
            {
                if (userID != 0)
                {
                    return userService.GetOneUser(userID);
                }

                return new CruizeUser();
            }
        }

        private IUserService userService;

        public LoginLogoutUser(IUserService service, IProfilePicture iPicture)
        {
            userService = service;
            _iPicture = iPicture;

        }
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            return CheckLogin();
        }

        public IActionResult OnPostLogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("LoginLogoutUser");
        }

        IActionResult CheckLogin()
        {
            string password;
            string userName;
            userName = Request.Form["Email"];
            password = Request.Form["Password"];
            bool checkLogin = userService.CheckPassword(userName, password);
            if (checkLogin)
            {
                HttpContext.Session.SetInt32("Login", 1);
                HttpContext.Session.SetString("UserName", userName);
                HttpContext.Session.SetInt32("UserID", userService.GetUserId(userName));

                if (userService.CheckDriver(userService.GetUserId(userName)))
                {
                    HttpContext.Session.SetInt32("Driver", 1);
                }

                if (userName == "admin@easj.dk")
                {
                    HttpContext.Session.SetInt32("Admin", 1);
                }

                return Redirect("Index");
            }

            else
            {
                HttpContext.Session.SetInt32("Login", 0);
                HttpContext.Session.SetString("UserName", "");
                return Page();
            }
        }

        public IActionResult OnPostRemoveDriver()
        {
            userService.DeleteDriver(userID);
            HttpContext.Session.SetInt32("Driver", 0);
            return Page();
        }

        public IActionResult OnPostAddDriver()
        {
            userService.AddDriver(LoggedInUser);
            HttpContext.Session.SetInt32("Driver", 1);
            return Page();
        }
    }
}