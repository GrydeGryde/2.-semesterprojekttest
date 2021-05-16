using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2._semesterprojekttest.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using _2._semesterprojekttest.Models;

namespace _2._semesterprojekttest.Pages.Shared
{
    public class LayoutModel : PageModel
    {
        private IProfilePicture _iPicture;

        public Picture ProfilePicture
        {
            get;
            set;
        }


        public LayoutModel(IProfilePicture Billed)
        {
            _iPicture = Billed;
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

        public void OnGet()
        {
            ProfilePicture = _iPicture.GetProfilePicture(userID);
        }

    }
}
