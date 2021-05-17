using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using _2._semesterprojekttest.Interfaces;
using _2._semesterprojekttest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace _2._semesterprojekttest.Pages
{
    public class ImageUploadTESTModel : PageModel
    {
        private readonly ILogger<ImageUploadTESTModel> _logger;
        private Picture _picture;
        private List<Picture> _pictures;
        private ImageTest _image;
        private List<ImageTest> _images;
        private GrydenDBContext db = new GrydenDBContext();
        private IProfilePicture _iPicture;
        public Picture ProfilePicture { get; set; }

        private const string ConnectionString =
            "Data Source=alex-gryden-db.database.windows.net;Initial Catalog=\"Gryden DB\";Persist Security Info=True;User ID=adminlogin;Password=secret1!";

        public ImageUploadTESTModel(IProfilePicture billedinterface)
        {
            _iPicture = billedinterface;
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

        public void OnGet()
        {
            ProfilePicture = _iPicture.GetProfilePicture(userID);
        }

        public void OnPost(Picture pic)
        {
            GrydenDBContext db = new GrydenDBContext();
            {
                string path = @"C:\Users\grydg\OneDrive\Billeder\";
                FileStream b1 = System.IO.File.OpenRead(path + Request.Form["Picture1"]);
                long b1size = b1.Length;
                byte[] billedBytes = new byte[b1size];
                int noOfBytes = b1.Read(billedBytes, 0, (int) b1size);


                pic.UserId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
                pic.FileType = "jpg";
                pic.Picture1 = billedBytes;
                pic.TypeId = Convert.ToInt32(Request.Form["Bil/profil"]);
                _iPicture.AddPicture(pic);
                ProfilePicture = pic;
            }
        }
    }
}
