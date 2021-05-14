using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2._semesterprojekttest.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using _2._semesterprojekttest.Models;
using Microsoft.AspNetCore.Http;

namespace _2._semesterprojekttest.Pages
{
    public class AdminReportedUsersModel : PageModel
    {
        private IUserService _userService;
        private IReportService _reportService;

        public List<Report> AllReports;
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

        public AdminReportedUsersModel(IUserService userService, IReportService reportService)
        {
            _userService = userService;
            _reportService = reportService;
        }


        public void OnGet()
        {
            AllReports = _reportService.ReportedUsers();
        }

        public IActionResult OnPostBan(string email, int id)
        {
            BannedUser bannedUser = new BannedUser();
            bannedUser.BannedEmail = email;
            _reportService.AddBan(bannedUser);
            _userService.DeleteUser(id);
            AllReports = _reportService.ReportedUsers();
            return Page();
        }


        public CruizeUser GetReportedUser(int id)
        {
            CruizeUser reportedUser = _userService.GetOneUser(id);
            return reportedUser;
        }
    }
}
