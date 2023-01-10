using System;
using System.Collections.Generic;
using CustomExceptions;

namespace Models;

public enum RelationshipStatus                            
{
    Pending, Accepted, Removed, Blocked
}

public class FriendsStatusWithEnum
{ 
    public int RelationshipId { get; set; }
    public int UserIdFrom { get; set; }
    public int UserIdTo { get; set; }
    public RelationshipStatus Status { get; set; }
    
    public string StatusToString(RelationshipStatus Status)                                   
    {
        Dictionary<RelationshipStatus, string> dictStatus = new Dictionary<RelationshipStatus, string>()
        {
            {RelationshipStatus.Pending, "Pending"},
            {RelationshipStatus.Accepted, "Accepted"},
            {RelationshipStatus.Removed, "Removed"},
            {RelationshipStatus.Blocked, "Blocked"},
        };

        if(dictStatus.ContainsKey(Status))
        {
            return dictStatus[Status];
        }
        throw new StatusNotFound();
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