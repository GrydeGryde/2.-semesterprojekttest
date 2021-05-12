using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using _2._semesterprojekttest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        private const string ConnectionString =
            "Data Source=alex-gryden-db.database.windows.net;Initial Catalog=\"Gryden DB\";Persist Security Info=True;User ID=adminlogin;Password=secret1!";

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
            //var checkforpic = db.ImageTests.Find(HttpContext.Session.GetInt32("UserID"));
            //if (checkforpic != null)
            //{

            //}
        }

        public void OnPost()
        {
            string path = @"C:\Users\grydg\OneDrive\Billeder\";
            FileStream b1 = System.IO.File.OpenRead(path + Request.Form["Picture1"]);
            long b1size = b1.Length;
            byte[] billedBytes = new byte[b1size];
            int noOfBytes = b1.Read(billedBytes, 0, (int)b1size);

            GrydenDBContext db = new GrydenDBContext();
            Picture pic = new Picture();

            pic.UserId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
            pic.FileType = "jpg";
            pic.Picture1 = billedBytes;
            pic.TypeId = Convert.ToInt32(Request.Form["Bil/profil"]);
            db.Pictures.Add(pic);
            db.SaveChanges();
        }
    }
}
