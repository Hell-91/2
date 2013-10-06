using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NCRSocialNetwork.Models;

namespace NCRSocialNetwork.ViewModels
{
    public class ClubViewModel : BaseViewModel
    {
        public Club Club { get; set; }

        public List<Event> Events { get; set; }

        public List<Comment> Comments { get; set; }

        public List<EventLikeDislike> EventLikeDislikes { get; set; }

        public bool ClubSubscribed { get; set; }

        public int UsersSubscribed { get; set; }
    }
}
