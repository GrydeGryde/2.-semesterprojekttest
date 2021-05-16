using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using _2._semesterprojekttest.Interfaces;
using _2._semesterprojekttest.Models;
using Microsoft.AspNetCore.Http;

namespace _2._semesterprojekttest.Services
{
    public class PictureService : IProfilePicture
    {
        private GrydenDBContext db = new GrydenDBContext();

        public void AddPicture(Picture pic)
        {
            var idcheck = db.Pictures.Where(i => i.UserId == pic.UserId && i.TypeId == pic.TypeId).ToList();
            
                if (idcheck.Count()==1)
                {
                    foreach (var picture in idcheck)
                    {
                        pic.PictureId = picture.PictureId;
                        db.Update(pic);
                        db.SaveChanges();
                    }
                }
                else
                {
                    db.Pictures.Add(pic);
                    db.SaveChanges();
                }
            
        }

        public void UpdatePicture(Picture pic)
        {
            foreach (Picture picture in db.Pictures.ToList())
            {
                if ((picture.TypeId == pic.TypeId) && (picture.UserId == pic.UserId))
                {
                    pic.PictureId = picture.PictureId;
                    db.Update(pic);
                }
            }
        }

        public void DeletePicture(int userID, int typeID)
        {
        }
    }
}