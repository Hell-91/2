using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NCRSocialNetwork.ViewModels;
using NCRSocialNetwork.Models;
using System.Data;
using System.Data.Entity;

namespace NCRSocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private NCRSNEntity dbConn = new NCRSNEntity();

        public ActionResult Index()
        {
            var viewModel = new HomeViewModel() { 
               Clubs = dbConn.Clubs.ToList(),
               Events = dbConn.Events.ToList(),
               Comments = dbConn.Comments.ToList(),
               EventLikeDislikes = dbConn.EventLikeDislikes.ToList(),
               BaseClub = dbConn.Clubs.ToList(),
               BaseUser = dbConn.Users.Where(u => u.UserId == 7).First()
            };
            
            return View(viewModel);
        }

        [HttpPost]
        public PartialViewResult SubmitComment(int UserId, string UserName, string UserComment, int EventId)
        {
            var eventcomment = new Comment() { 
                CommentUserId = UserId,
                CommentDescription = UserComment,
                CommentKeyId = EventId,
                CommentCreatedDate = DateTime.Now
            };            
            if (ModelState.IsValid)
            {
                dbConn.Comments.Add(eventcomment);
                dbConn.SaveChanges();
            }
            ViewBag.Comment = UserName + ": " + UserComment;
            return PartialView("_EventCommentView");
        }

        [HttpPost]
        public PartialViewResult LikeEvent(int UserId, int EventId)
        {
            var eventlike = new EventLikeDislike(){
                EventLikeDislikeUserId = UserId,
                EventLikeDislikeEventId = EventId,
                EventLikeDislikeValue = 1
            };
            if (ModelState.IsValid)
            {
                dbConn.EventLikeDislikes.Add(eventlike);
                dbConn.SaveChanges();
            }
            ViewBag.Likes = dbConn.EventLikeDislikes.Count(e => e.EventLikeDislikeEventId == EventId && e.EventLikeDislikeValue == 1);
            ViewBag.Dislikes = dbConn.EventLikeDislikes.Count(e => e.EventLikeDislikeEventId == EventId && e.EventLikeDislikeValue == 0);
            return PartialView("_LikeDislikeView");
        }

        [HttpPost]
        public PartialViewResult DislikeEvent(int UserId, int EventId)
        {
            var eventlike = new EventLikeDislike()
            {
                EventLikeDislikeUserId = UserId,
                EventLikeDislikeEventId = EventId,
                EventLikeDislikeValue = 0
            };
            if (ModelState.IsValid)
            {
                dbConn.EventLikeDislikes.Add(eventlike);
                dbConn.SaveChanges();
            }
            ViewBag.Likes = dbConn.EventLikeDislikes.Count(e => e.EventLikeDislikeEventId == EventId && e.EventLikeDislikeValue == 1);
            ViewBag.Dislikes = dbConn.EventLikeDislikes.Count(e => e.EventLikeDislikeEventId == EventId && e.EventLikeDislikeValue == 0);
            return PartialView("_LikeDislikeView");
        }

        [HttpPost]
        public PartialViewResult UnlikeEvent(int UserId, int EventId)
        {
            var eventLikeDislike = dbConn.EventLikeDislikes.Where(e => e.EventLikeDislikeEventId == EventId && e.EventLikeDislikeUserId == UserId).FirstOrDefault();
            EventLikeDislike eventlikedislike = dbConn.EventLikeDislikes.Find(eventLikeDislike.EventLikeDislikeId);
            dbConn.EventLikeDislikes.Remove(eventlikedislike);
            dbConn.SaveChanges();

            ViewBag.Likes = dbConn.EventLikeDislikes.Count(e => e.EventLikeDislikeEventId == EventId && e.EventLikeDislikeValue == 1);
            ViewBag.Dislikes = dbConn.EventLikeDislikes.Count(e => e.EventLikeDislikeEventId == EventId && e.EventLikeDislikeValue == 0);
            return PartialView("_LikeDislikeView");
        }
    }
}
