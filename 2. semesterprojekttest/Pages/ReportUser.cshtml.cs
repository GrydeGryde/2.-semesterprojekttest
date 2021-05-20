using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2._semesterprojekttest.Interfaces;
using _2._semesterprojekttest.Models;
using _2._semesterprojekttest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _2._semesterprojekttest.Pages
{
    public class ReportUserModel : PageModel
    {
        private IReportService _reportService;
        private IUserService _userService;
        private IProfilePicture _iPicture;
        public CruizeUser ReportedUser { get; set; }
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
        public Picture GetPicture(int id)
        {
            return _iPicture.GetProfilePicture(id);
        }
        public ReportUserModel(IReportService service, IUserService userService, IProfilePicture pictureservice)
        {
            _reportService = service;
            _userService = userService;
            _iPicture = pictureservice;
        }

        public string ReportStatus { get; set; }
        public void OnGet(int id)
        {
            ProfilePicture = _iPicture.GetProfilePicture(userID);
            ReportedUser = _userService.GetOneUser(id);
        }
        public IActionResult OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                Report report = new Report();
                report.Reporter = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
                report.Reported = id;
                report.Message = Request.Form["Message"];
                ReportStatus = "Your Report was sent";
                _reportService.AddReport(report);
                return RedirectToPage("DummyReportPage", new { ReportStatus });
            }
            //Tilføj Respons besked hvis til knappen
            return RedirectToPage("DummyReportPage", ReportStatus);
        }
    }
}
