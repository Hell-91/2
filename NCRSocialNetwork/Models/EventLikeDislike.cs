//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NCRSocialNetwork.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EventLikeDislike
    {
        public EventLikeDislike()
        {
            this.EventLikeDislikeValue = -1;
        }
    
        public int EventLikeDislikeId { get; set; }
        public int EventLikeDislikeEventId { get; set; }
        public int EventLikeDislikeUserId { get; set; }
        public int EventLikeDislikeValue { get; set; }
    
        public virtual Event Event { get; set; }
        public virtual User User { get; set; }
    }
}
