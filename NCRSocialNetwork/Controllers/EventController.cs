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
    public class EventController : Controller
    {
        private NCRSNEntity db = new NCRSNEntity();

        //
        // GET: /Event/

        public ActionResult Index()
        {
            var Events = db.Events.Include(e => e.User).Include(e => e.Club).Include(e => e.Key);
            return View(Events.ToList());
        }

        //
        // GET: /Event/Details/5

        public ActionResult Details(int id = 0)
        {
            Event requestedEvent = db.Events.Find(id);

            EventViewModel Event = new EventViewModel() { 
                Event = requestedEvent,
                BaseClub = db.Clubs.ToList(),
                EventLikeDislikes = db.EventLikeDislikes.Where(e => e.EventLikeDislikeEventId == id).ToList(),
                BaseUser = db.Users.Where(u => u.UserId == 7).First()
            };
            if (Event == null)
            {
                return HttpNotFound();
            }
            return View(Event);
        }

        //
        // GET: /Event/Create

        public ActionResult Create()
        {
            ViewBag.EventCreatedBy = new SelectList(db.Users, "UserId", "UserFirstName");
            ViewBag.EventClubId = new SelectList(db.Clubs, "ClubId", "ClubName");
            ViewBag.EventId = new SelectList(db.Keys, "KeyId", "KeyType");
            return View();
        }

        //
        // POST: /Event/Create

        [HttpPost]
        public ActionResult Create(Event Event)
        {
            Event.EventCreatedTime = DateTime.Now;

            Key key = new Key()
            {
                KeyType = "E",
                KeyCreatedDate = DateTime.Now
            };

            if (ModelState.IsValid)
            {
                db.Keys.Add(key);
                db.SaveChanges();
            }

            Event.EventId = key.KeyId;

            if (ModelState.IsValid)
            {
                db.Events.Add(Event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventCreatedBy = new SelectList(db.Users, "UserId", "UserFirstName", Event.EventCreatedBy);
            ViewBag.EventClubId = new SelectList(db.Clubs, "ClubId", "ClubName", Event.EventClubId);
            ViewBag.EventId = new SelectList(db.Keys, "KeyId", "KeyType", Event.EventId);
            return View(Event);
        }

        //
        // GET: /Event/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Event Event = db.Events.Find(id);
            if (Event == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventCreatedBy = new SelectList(db.Users, "UserId", "UserFirstName", Event.EventCreatedBy);
            ViewBag.EventClubId = new SelectList(db.Clubs, "ClubId", "ClubName", Event.EventClubId);
            ViewBag.EventId = new SelectList(db.Keys, "KeyId", "KeyType", Event.EventId);
            return View(Event);
        }

        //
        // POST: /Event/Edit/5

        [HttpPost]
        public ActionResult Edit(Event Event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventCreatedBy = new SelectList(db.Users, "UserId", "UserFirstName", Event.EventCreatedBy);
            ViewBag.EventClubId = new SelectList(db.Clubs, "ClubId", "ClubName", Event.EventClubId);
            ViewBag.EventId = new SelectList(db.Keys, "KeyId", "KeyType", Event.EventId);
            return View(Event);
        }

        //
        // GET: /Event/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Event Event = db.Events.Find(id);
            if (Event == null)
            {
                return HttpNotFound();
            }
            return View(Event);
        }

        //
        // POST: /Event/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Event Event = db.Events.Find(id);
            db.Events.Remove(Event);
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