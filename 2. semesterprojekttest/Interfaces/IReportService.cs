using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2._semesterprojekttest.Models;

namespace _2._semesterprojekttest.Interfaces
{
    public interface IReportService
    {
        void AddReport(Report report);
        List<Report> ReportedUsers();
        void AddBan(BannedUser bannedUser);
        List<BannedUser> BannedUsers();
        void DeleteBan(BannedUser bannedUser);
        void DeleteReport(int id);
    }
}
