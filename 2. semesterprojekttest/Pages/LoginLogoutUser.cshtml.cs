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
        public int validUser
        {
            get { return Convert.ToInt32(HttpContext.Session.GetInt32("Login")); }
        }
        public int userID
        {
            get { return Convert.ToInt32(HttpContext.Session.GetInt32("UserID")); }
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

        public LoginLogoutUser(IUserService service)
        {
            userService = service;
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
            return LogOut();
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
                return Redirect("~/Calendar");
            }
            else
            {
                HttpContext.Session.SetInt32("Login", 0);
                HttpContext.Session.SetString("UserName", "");
                return Page();
            }
        }
        IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("LoginLogoutUser");
        }
    }
}