using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2._semesterprojekttest.Models;

namespace _2._semesterprojekttest.Interfaces
{
    public interface IProfilePicture
    {
        void AddPicture(Picture pic);
        void DeletePicture(int userid, int typeID);
        Picture GetProfilePicture(int userid);
        Picture GetCarPicture(int userid);
    }
}
