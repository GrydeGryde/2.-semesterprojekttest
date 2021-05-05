using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using _2._semesterprojekttest.Services;
using _2._semesterprojekttest.Models;
using _2._semesterprojekttest.Interfaces;

namespace _2._semesterprojekttest.Pages
{
    public class CreateUserModel : PageModel
    {
        private IUserService userService;

        public CreateUserModel(IUserService service)
        {
            userService = service;
        }

        public string EmailError { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            List<CruizeUser> users = userService.GetAllUsers();
            string UserEmail = Request.Form["Email".ToLower()];
            bool UserStatus = false;

            if (ModelState.IsValid)
            {
                if (!UserEmail.Contains("@easj.dk") || !UserEmail.Contains("@edu.easj.dk") || !UserEmail.Contains("@zealand.dk"))
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
                if (Request.Form["userStatus"] == "Driver")
                {
                    UserStatus = true;
                }
            }

            CruizeUser cruizer = new CruizeUser();
            cruizer.FirstName = Request.Form["First Name"];
            cruizer.LastName = Request.Form["Last Name"];
            cruizer.Address = Request.Form["Address"];
            cruizer.Email = Request.Form["Email"];
            cruizer.Password = Request.Form["Password"];

            userService.AddUser(cruizer);
            
            if (UserStatus == true)
            {
                userService.AddDriver(cruizer);
            }
        }

    }
}
