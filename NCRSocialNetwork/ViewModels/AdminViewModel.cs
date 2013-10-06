using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NCRSocialNetwork.Models;

namespace NCRSocialNetwork.ViewModels
{
    public class AdminViewModel : BaseViewModel
    {
        public List<Club> Clubs { get; set; }

        public List<Event> Events { get; set; }

        public List<EventLikeDislike> EventLikeDislikes { get; set; }

        public List<EventRequest> EventRequests { get; set; }

        public List<User> Users { get; set; }
    }
}