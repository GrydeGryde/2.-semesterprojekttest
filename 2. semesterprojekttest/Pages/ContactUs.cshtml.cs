using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using _2._semesterprojekttest.Interfaces;
using _2._semesterprojekttest.Models;
using Microsoft.AspNetCore.Http;

namespace _2._semesterprojekttest.Pages
{
    public class ContactUsModel : PageModel
    {
        private IProfilePicture _iPicture;
        public Picture ProfilePicture{ get; set; }
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
        public ContactUsModel(IProfilePicture billed)
        {
            _iPicture = billed;
        }
        public void Onget()
        {
            ProfilePicture = _iPicture.GetProfilePicture(userID);
        }
    }
}
