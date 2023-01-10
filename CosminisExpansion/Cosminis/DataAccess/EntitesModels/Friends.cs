using System;
using System.Collections.Generic;
using Models;

namespace DataAccess.Entities
{
    public partial class Friends
    { 
        public Friends()
        {
            
        }

        public Friends(Models.FriendsStatusWithEnum Friends)
        {
            this.RelationshipId = Friends.RelationshipId;
            this.UserIdFrom = Friends.UserIdFrom;
            this.UserIdTo = Friends.UserIdTo;
            this.Status = Friends.StatusToString(Friends.Status);
        }

        public override string ToString()
        { 
            return 
                $"RelationshipId: {this.RelationshipId}, " + 
                $"UserId of requester: {this.UserIdFrom}, " + 
                $"UserId of responder: {this.UserIdTo}, " + 
                $"Status: {this.Status}"; 
        }    
    }
}
