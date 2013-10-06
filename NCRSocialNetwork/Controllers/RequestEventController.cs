using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NCRSocialNetwork.Models;

namespace NCRSocialNetwork.Controllers
{
    public class RequestEventController : Controller
    {
        private NCRSNEntity db = new NCRSNEntity();

        //
        // GET: /RequestEvent/

        public ActionResult Index()
        {
            var eventrequests = db.EventRequests.Include(e => e.Club).Include(e => e.User);
            return View(eventrequests.ToList());
        }

        //
        // GET: /RequestEvent/Details/5

        public ActionResult Details(int id = 0)
        {
            EventRequest eventrequest = db.EventRequests.Find(id);
            if (eventrequest == null)
            {
                return HttpNotFound();
            }
            return View(eventrequest);
        }

        //
        // GET: /RequestEvent/Create

        public ActionResult NewEvent()
        {
            ViewBag.EventRequestClubId = new SelectList(db.Clubs, "ClubId", "ClubName");
            ViewBag.EventRequestUserId = new SelectList(db.Users, "UserId", "UserFirstName");
            return View();
        }

        //
        // POST: /RequestEvent/Create

        [HttpPost]
        public ActionResult NewEvent(EventRequest eventrequest)
        {
            eventrequest.EventRequestDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.EventRequests.Add(eventrequest);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.EventRequestClubId = new SelectList(db.Clubs, "ClubId", "ClubName", eventrequest.EventRequestClubId);
            ViewBag.EventRequestUserId = new SelectList(db.Users, "UserId", "UserFirstName", eventrequest.EventRequestUserId);
            return View(eventrequest);
        }

        //
        // GET: /RequestEvent/Edit/5

        public ActionResult Edit(int id = 0)
        {
            EventRequest eventrequest = db.EventRequests.Find(id);
            if (eventrequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventRequestClubId = new SelectList(db.Clubs, "ClubId", "ClubName", eventrequest.EventRequestClubId);
            ViewBag.EventRequestUserId = new SelectList(db.Users, "UserId", "UserFirstName", eventrequest.EventRequestUserId);
            return View(eventrequest);
        }

        //
        // POST: /RequestEvent/Edit/5

        [HttpPost]
        public ActionResult Edit(EventRequest eventrequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventrequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventRequestClubId = new SelectList(db.Clubs, "ClubId", "ClubName", eventrequest.EventRequestClubId);
            ViewBag.EventRequestUserId = new SelectList(db.Users, "UserId", "UserFirstName", eventrequest.EventRequestUserId);
            return View(eventrequest);
        }

        //
        // GET: /RequestEvent/Delete/5

        public ActionResult Delete(int id = 0)
        {
            EventRequest eventrequest = db.EventRequests.Find(id);
            if (eventrequest == null)
            {
                return HttpNotFound();
            }
            return View(eventrequest);
        }

        //
        // POST: /RequestEvent/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            EventRequest eventrequest = db.EventRequests.Find(id);
            db.EventRequests.Remove(eventrequest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}