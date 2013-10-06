using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NCRSocialNetwork.Models;

namespace NCRSocialNetwork.ViewModels
{
    public class EventViewModel : BaseViewModel
    {
        public Event Event { get; set; }

        public List<Comment> Comments { get; set; }

        public List<EventLikeDislike> EventLikeDislikes { get; set; }

    }
}