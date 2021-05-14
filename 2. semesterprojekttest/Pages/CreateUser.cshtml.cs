using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using _2._semesterprojekttest.Services;
using _2._semesterprojekttest.Models;
using _2._semesterprojekttest.Interfaces;
using Microsoft.AspNetCore.Http;

namespace _2._semesterprojekttest.Pages
{
    public class CreateUserModel : PageModel
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

        private IUserService _userService;
        private IReportService _reportService;


        public CreateUserModel(IUserService service, IReportService reportService)

        {
            _userService = service;
            _reportService = reportService;
        }

        public string EmailError { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            List<CruizeUser> users = _userService.GetAllUsers();
            string userEmail = Request.Form["Email".ToLower()];
            bool userStatus = false;

            foreach (BannedUser bannedUser in _reportService.BannedUsers())
            {
                if (userEmail == bannedUser.BannedEmail)
                {
                    EmailError = "This user has been banned from the application";
                    return;
                }
            }

            if (ModelState.IsValid)
            {
                if (!userEmail.Contains("@easj.dk") && !userEmail.Contains("@edu.easj.dk") && !userEmail.Contains("@zealand.dk"))
                {
                    EmailError = "You have to use a Zealand email";
                    return;
                }
                foreach (CruizeUser Cruizer in users)
                {
                    if (Cruizer.Email.ToLower() == Request.Form["Email".ToLower()])
                    {
                        EmailError = "A user with this email already exists";
                        return;
                    }
                }
                if (Request.Form["userStatus"] == "driver")
                {
                    userStatus = true;
                }
            }

            CruizeUser cruizer = new CruizeUser();
            cruizer.FirstName = Request.Form["First Name"];
            cruizer.LastName = Request.Form["Last Name"];
            cruizer.Address = Request.Form["Address"];
            cruizer.Email = Request.Form["Email"];
            cruizer.Password = Request.Form["Password"];
            cruizer.Zipcode = Convert.ToInt32(Request.Form["Zipcode"]);

            _userService.AddUser(cruizer);
            
            if (userStatus == true)
            {
                _userService.AddDriver(cruizer);
            }
        }

    }
}
