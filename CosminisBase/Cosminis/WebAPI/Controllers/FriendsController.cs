using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Controllers;

public class FriendsController
{
	private readonly FriendServices _friendServices;

    public FriendsController(FriendServices friendServices)
    {
        _friendServices = friendServices;
    }

    public IResult ViewAllRelationships()
    {
    	try
    	{
    		List<Friends> friendsList = _friendServices.ViewAllRelationships();
    		return Results.Ok(friendsList); 
    	}
    	catch(ResourceNotFound)
        {
            return Results.NotFound("Something went very wrong here."); 
        }	
    }

    public IResult ViewAllFriends(int userIdToLookup)
    {
    	try
    	{
    		List<Friends> friendsList = _friendServices.ViewAllFriends(userIdToLookup);
    		return Results.Ok(friendsList); 
    	}
        catch(UserNotFound)
        {
            return Results.NotFound("No user with this ID exists."); 
        }	
        catch(RelationshipNotFound)
        {
            return Results.NotFound("This user has no friends!"); 
        }	
    	catch(ResourceNotFound)
        {
            return Results.NotFound("Something went wrong with this request."); 
        }	
    }

    public IResult SearchByRelationshipId(int relationshipId)
    {
    	try
    	{
    		Friends relationship = _friendServices.SearchByRelationshipId(relationshipId);
    		return Results.Ok(relationship); 
    	}
    	catch(ResourceNotFound)
        {
            return Results.NotFound("This relationship does not exist."); 
        }	
    }

    public IResult ViewRelationshipsByStatus(string status)
    {
    	try
    	{
    		List<Friends> friendsList = _friendServices.ViewRelationShipsByStatus(status);
    		return Results.Ok(friendsList); 
    	}
        catch(UserNotFound)
        {
            return Results.NotFound("No user with this ID exists."); 
        }        
        catch(RelationshipNotFound)
        {
            return Results.NotFound("This user has no friends!"); 
        }	         
    	catch(ResourceNotFound)
        {
            return Results.NotFound("Something went very wrong here."); 
        }	
    }

    public IResult CheckRelationshipStatusByUserId(int searchingId, string status)
    {
    	try
    	{
    		List<Friends> friendsList = _friendServices.CheckRelationshipStatusByUserId(searchingId, status);
    		return Results.Ok(friendsList); 
    	}
        catch(UserNotFound)
        {
            return Results.NotFound("No user with this ID exists."); 
        }        
        catch(RelationshipNotFound)
        {
            return Results.NotFound("This user has no friends!"); 
        }        
    	catch(ResourceNotFound)
        {
            return Results.NotFound("This information is not in our database."); 
        }	
    }  

    public IResult FriendsByUserIds(int searchingUserId, int user2BeSearchedFor)
    {
    	try
    	{
    		Friends friendsReturn = _friendServices.FriendsByUserIds(searchingUserId, user2BeSearchedFor);
    		return Results.Ok(friendsReturn); 
    	}
        catch(UserNotFound)
        {
            return Results.NotFound("No user with this ID exists."); 
        }
        catch(RelationshipNotFound)
        {
            return Results.NotFound("This user has no friends!"); 
        }	        	        
    	catch(ResourceNotFound)
        {
            return Results.NotFound("These users have no established relationship."); 
        }	
    }

    public IResult EditStatus(int editingUserID, int user2BeEdited, string status)
    {
    	try
    	{
    		Friends editFriendship = _friendServices.EditFriendship(editingUserID, user2BeEdited, status);
    		return Results.Created("/Friends/EditStatus", editFriendship); 
    	}
        catch(UserNotFound)
        {
            return Results.NotFound("No user with this ID exists."); 
        }        
        catch(RelationshipNotFound)
        {
            return Results.NotFound("This user has no friends!"); 
        }        
    	catch(ResourceNotFound)
        {
            return Results.NotFound("This user doesn't exist in our system."); 
        }	
    }    

    public IResult CheckRelationshipStatusByUsername(string username, string status)
    {
    	try
    	{
    		List<Friends> friendsList = _friendServices.CheckRelationshipStatusByUsername(username, status);
    		return Results.Ok(friendsList); 
    	}
        catch(UserNotFound)
        {
            return Results.NotFound("No user with this ID exists."); 
        }
        catch(RelationshipNotFound)
        {
            return Results.NotFound("This user has no friends!"); 
        }        
    	catch(ResourceNotFound)
        {
            return Results.NotFound("This user has no relationships with this status."); 
        }	
    }      

    public IResult AddFriendByUserId(int userToAddId, int requestReceiver)
    {
    	try
    	{
    		Friends newFriends = _friendServices.AddFriendByUserId(userToAddId, requestReceiver);
    		return Results.Created("/Friends/AddFriend", newFriends); 
    	}
        catch(UserNotFound)
        {
            return Results.NotFound("No user with this ID exists."); 
        }        
    	catch(DuplicateFriends)
        {
            return Results.Conflict("You are already friends!!"); 
        }
    	catch(BlockedUser)
        {
            return Results.Conflict("The information you have entered is not valid"); 
        }  
        catch(PendingFriends)
        {
            return Results.Conflict("This friendship is already pending.");
        }               	
    }

    public IResult AddFriendByUsername(string userToAdd, string requestReceiver)
    {
    	try
    	{
    		Friends newFriends = _friendServices.AddFriendByUsername(userToAdd, requestReceiver);
    		return Results.Created("/Friends/AddFriendByUsername", newFriends); 
    	}
        catch(UserNotFound)
        {
            return Results.NotFound("No user with this username exists."); 
        }        
    	catch(DuplicateFriends)
        {
            return Results.Conflict("You are already friends!!"); 
        }	
    	catch(BlockedUser)
        {
            return Results.Conflict("The information you have entered is not valid"); 
        } 
        catch(PendingFriends)
        {
            return Results.Conflict("This friendship is already pending.");
        }                
    }     
}