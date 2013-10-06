using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NCRSocialNetwork.Models;

namespace NCRSocialNetwork.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public User Profile { get; set; }

        public List<Comment> Comments { get; set; }

        public List<EventLikeDislike> EventLikeDislikes { get; set; }
    }
}