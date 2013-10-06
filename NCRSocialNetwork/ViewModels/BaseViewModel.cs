using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NCRSocialNetwork.Models;

namespace NCRSocialNetwork.ViewModels
{
    public class BaseViewModel
    {
        public List<Club> BaseClub { get; set; }

        public User BaseUser { get; set; }
    }
}