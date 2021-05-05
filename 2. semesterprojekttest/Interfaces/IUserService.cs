using System;
using System.Collections.Generic;
using _2._semesterprojekttest.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace _2._semesterprojekttest.Interfaces
{
    public interface IUserService
    {
        List<CruizeUser> GetAllUsers();

        CruizeUser GetOneUser(int id);

        bool AddUser(CruizeUser user);

        bool AddDriver(CruizeUser user);

        CruizeUser DeleteUser(int id);

        bool UpdateUser(int id, CruizeUser user);

        public bool CheckPassword(string userName, string password);

        public string PasswordHash(string userName, string password);

        int GetUserId(string email);
    }
}
