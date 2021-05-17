using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2._semesterprojekttest.Interfaces;
using _2._semesterprojekttest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace _2._semesterprojekttest.Pages
{
    public class ProfileModel : PageModel
    {
        private IProfilePicture _iPicture;
        private readonly ILogger<ProfileModel> _logger;
        private Picture _picture;
        private List<Picture> _pictures;
        private GrydenDBContext db = new GrydenDBContext();
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

        public Picture Picture
        {
            get => _picture;
            set => _picture = value;
        }


        public List<Picture> Pictures
        {
            get => _pictures;
            set => _pictures = value;
        }


        public ProfileModel(ILogger<ProfileModel> logger, IProfilePicture pictureservice)
        {
            _logger = logger;
            _iPicture = pictureservice;
        }
        public void OnGet()
        {
            ProfilePicture = _iPicture.GetProfilePicture(userID);
            //var checkforpic = db.Pictures.Find(userID, Picture.UserId);
            var checkforpic = db.Pictures.Where(i => i.UserId==userID).ToList();
            if (checkforpic.Count() != 0)
            {
                _pictures = checkforpic;
            }

        }
    }
}
