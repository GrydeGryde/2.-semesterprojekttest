using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2._semesterprojekttest.Models;

namespace _2._semesterprojekttest.Interfaces
{
    public interface IRouteService
    {
        bool AddRoute(Route route);
        bool RemoveRoute(int routeid);
        bool UpdateRoute(int id, Route route);
        List<Route> GetAllRoutes();
        Route GetOneRoute(int id);
        bool AddRequest(Request request);
        Request DeleteRequest(int id);
        Request GetOneRequest(int id);
        List<Request> GetAllRequests(int id);
        bool AcceptRequest(int UserID, int RouteID);
        bool ReduceSpace(int RouteID);
        List<Route> GetAllPassengerRoutes(int id);
        List<Route> GetAllDriverRoutes(int id);
        bool CheckRequest(int UserID, int RouteID);
        public List<CruizeUser> GetAllPassengerUsers(int routeid);
        bool RemovePassengerUser(int userid, int routeid);
        bool IncreaseSpace(int RouteID);
        string GetDay(int day);
    }
}
