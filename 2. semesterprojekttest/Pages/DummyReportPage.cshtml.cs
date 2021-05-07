using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2._semesterprojekttest.Interfaces;
using _2._semesterprojekttest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _2._semesterprojekttest.Pages
{
    public class DummyReportPageModel : PageModel
    {
        public CruizeUser user;
        public List<CruizeUser> _listeUsers;
        private IUserService _userService;

        public DummyReportPageModel(IUserService service)
        {
            _userService = service;
        }
        public void OnGet()
        {
            _listeUsers = _userService.GetAllUsers();
        }
    }
}
