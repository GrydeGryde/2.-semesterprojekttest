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
        public CruizeUser ReportedUser { get; set; }
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

        public ReportUserModel(IReportService service, IUserService userService)
        {
            _reportService = service;
            _userService = userService;
        }
        public void OnGet(int id)
        {
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
                
                _reportService.AddReport(report);
                return RedirectToPage("Index");
            }
            //Tilføj Respons besked hvis til knappen
            return RedirectToPage("Index");
        }
    }
}
