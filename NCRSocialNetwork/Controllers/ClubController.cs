using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NCRSocialNetwork.Models;
using NCRSocialNetwork.ViewModels;
using System.IO;

namespace NCRSocialNetwork.Controllers
{
    public class ClubController : Controller
    {
        private NCRSNEntity db = new NCRSNEntity();

        //
        // GET: /Club/

        public ActionResult Index()
        {
            var clubs = db.Clubs.Include(c => c.User).Include(c => c.Key);
            return View(clubs.ToList());
        }

        //
        // GET: /Club/Details/5

        public ActionResult Details(int id = 0)
        {
            Club club = db.Clubs.Find(id);
            if (club == null)
            {
                return HttpNotFound();
            }
            return View(club);
        }

        //
        // GET: /Club/5

        public ActionResult ClubPage(int clubId = 0, int userId = 7)
        {
            bool clubSubscribedFlag = false;
            if (db.ClubSubscribes.Where(c => c.ClubSubscribeClubId == clubId && c.ClubSubscribeUserId == userId).Count() > 0) {
                clubSubscribedFlag = true;
            }

            var events = db.Events.Where(e => e.EventClubId == clubId).ToList();
            var clubComments = new List<Comment>();
            foreach (var comment in db.Comments.ToList())
            {
                foreach (var e in events) {
                    if (comment.CommentKeyId == e.EventId) {
                        clubComments.Add(comment);
                        break;
                    }
                }
            }

            ClubViewModel club = new ClubViewModel() { 
                Club = db.Clubs.Find(clubId),
                Events = events,
                EventLikeDislikes = db.EventLikeDislikes.Where(e => e.Event.EventClubId == clubId).ToList(),
                Comments = clubComments,
                ClubSubscribed = clubSubscribedFlag,
                UsersSubscribed = db.ClubSubscribes.Where(c => c.ClubSubscribeClubId == clubId).Count(),
                BaseClub = db.Clubs.ToList(),
                BaseUser = db.Users.Where(u => u.UserId == 7).First()
            };
            
            if (club == null)
            {
                return HttpNotFound();
            }
            return View(club);
        }

        //
        // POST: /Clubs/Join

        [HttpPost]
        public ActionResult SubscribeClub(int clubId, int userId)
        {
            ClubSubscribe clubsubscribe = new ClubSubscribe()
            {
                ClubSubscribeClubId = clubId,
                ClubSubscribeUserId = userId
            };

            if (ModelState.IsValid)
            {
                db.ClubSubscribes.Add(clubsubscribe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClubSubscribeUserId = new SelectList(db.Users, "UserId", "UserFirstName", clubsubscribe.ClubSubscribeUserId);
            ViewBag.ClubSubscribeClubId = new SelectList(db.Clubs, "ClubId", "ClubName", clubsubscribe.ClubSubscribeClubId);
            return View(clubsubscribe);
        }

        //
        // POST: /TestJoin/Delete/5

        [HttpPost]
        public ActionResult UnsubscribeClub(int clubId, int userId)
        {
            int id = db.ClubSubscribes.Where(e => e.ClubSubscribeClubId == clubId && e.ClubSubscribeUserId == userId).First().ClubSubscribeId;
            ClubSubscribe clubsubscribe = db.ClubSubscribes.Find(id);
            db.ClubSubscribes.Remove(clubsubscribe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Club/Create

        public ActionResult Create()
        {
            ViewBag.ClubModerator = new SelectList(db.Users, "UserId", "UserFirstName");
            ViewBag.ClubId = new SelectList(db.Keys, "KeyId", "KeyType");
            return View();
        }

        [HttpPost]
        public PartialViewResult SubmitComment(int UserId, string UserName, string UserComment, int EventId)
        {
            var eventcomment = new Comment()
            {
                CommentUserId = UserId,
                CommentDescription = UserComment,
                CommentKeyId = EventId,
                CommentCreatedDate = DateTime.Now
            };
            if (ModelState.IsValid)
            {
                db.Comments.Add(eventcomment);
                db.SaveChanges();
            }
            ViewBag.Comment = UserName + ": " + UserComment;
            return PartialView("_EventCommentView");
        }


        //
        // POST: /Club/Create

        [HttpPost]
        public ActionResult Create(Club club)
        {
            Key key = new Key()
            {
                KeyType = "C",
                KeyCreatedDate = DateTime.Now
            };

            if (ModelState.IsValid)
            {
                db.Keys.Add(key);
                db.SaveChanges();
            }

            club.ClubId = key.KeyId; 
            
            if (ModelState.IsValid)
            {
                db.Clubs.Add(club);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClubModerator = new SelectList(db.Users, "UserId", "UserFirstName", club.ClubModerator);
            ViewBag.ClubId = new SelectList(db.Keys, "KeyId", "KeyType", club.ClubId);
            return View(club);
        }

        //
        // GET: /Club/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Club club = db.Clubs.Find(id);
            if (club == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClubModerator = new SelectList(db.Users, "UserId", "UserFirstName", club.ClubModerator);
            ViewBag.ClubId = new SelectList(db.Keys, "KeyId", "KeyType", club.ClubId);
            return View(club);
        }

        //
        // POST: /Club/Edit/5

        [HttpPost]
        public ActionResult Edit(Club club)
        {
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file];
                //Save file here
                if (hpf.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(hpf.FileName);
                    var path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    hpf.SaveAs(path);
                    if (file == "ClubThumbnailPath")
                    {
                        club.ClubThumbnailPath = "/Uploads/" + fileName;
                    }
                    else 
                    {
                        club.ClubImagePath = "/Uploads/" + fileName;
                    }
                }
            }

            NCRSNEntity tempDb = new NCRSNEntity();
            if (club.ClubImagePath == null) {
                club.ClubImagePath = tempDb.Clubs.Where(c => c.ClubId == club.ClubId).First().ClubImagePath;
            }

            if (club.ClubThumbnailPath == null)
            {
                club.ClubThumbnailPath = tempDb.Clubs.Where(c => c.ClubId == club.ClubId).First().ClubThumbnailPath;
            }

            tempDb.Dispose();

            if (ModelState.IsValid)
            {
                db.Entry(club).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(club.ClubId.ToString(), "Clubs");
            }
            ViewBag.ClubModerator = new SelectList(db.Users, "UserId", "UserFirstName", club.ClubModerator);
            ViewBag.ClubId = new SelectList(db.Keys, "KeyId", "KeyType", club.ClubId);
            return View(club);
        }

        //
        // GET: /Club/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Club club = db.Clubs.Find(id);
            if (club == null)
            {
                return HttpNotFound();
            }
            return View(club);
        }

        //
        // POST: /Club/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Club club = db.Clubs.Find(id);
            db.Clubs.Remove(club);
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