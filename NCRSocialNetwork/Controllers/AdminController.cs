using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NCRSocialNetwork.Models;
using NCRSocialNetwork.ViewModels;

namespace NCRSocialNetwork.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        private NCRSNEntity dbConn = new NCRSNEntity();

        public ActionResult Index()
        {
           
            var viewModel = new AdminViewModel()
            {
                Clubs = dbConn.Clubs.ToList(),
                Events = dbConn.Events.Include(e => e.Club).ToList(),
                EventLikeDislikes = dbConn.EventLikeDislikes.Include(e => e.Event).Include(e => e.User).ToList(),
                EventRequests = dbConn.EventRequests.ToList(),
                BaseClub = dbConn.Clubs.ToList(),
                BaseUser = dbConn.Users.Where(u => u.UserId == 7).First()
            };

            return View(viewModel);
        }

        //
        // GET: /Admin/Details/5

        public ActionResult Details(int id = 0)
        {
            Event Event = dbConn.Events.Find(id);
            if (Event == null)
            {
                return HttpNotFound();
            }
            return View(Event);
        }


        protected override void Dispose(bool disposing)
        {
            dbConn.Dispose();
            base.Dispose(disposing);
        }
    }
}