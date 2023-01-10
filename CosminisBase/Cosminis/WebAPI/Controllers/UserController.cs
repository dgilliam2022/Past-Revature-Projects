using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Controllers;

public class UserController
{
	private readonly UserServices _userServices;

    public UserController(UserServices userServices)
    {
        _userServices = userServices;
    }

    public IResult SearchFriend(string username)
    {
    	try
    	{
    		User userInfo = _userServices.SearchFriend(username);
    		return Results.Ok(userInfo); 
    	}
    	catch(UserNotFound)
        {
            return Results.NotFound("No user with that username was found."); 
        }	
    }

    public IResult LoginOrReggi(User user2Check)
    {
        try
    	{
    		User userInfo = _userServices.LoginOrReggi(user2Check);
    		return Results.Created($"Users/Find/{userInfo.UserId}", userInfo); //this needs to be created
    	}
    	catch(UserNotFound)
        {
            return Results.NotFound("No user with that username was found."); 
        }	
        catch(Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    public IResult SearchUserById(int user2Check)
    {
        try
    	{
    		User userInfo = _userServices.SearchUserById(user2Check);
    		return Results.Ok(userInfo); //this needs to be created
    	}
    	catch(UserNotFound)
        {
            return Results.NotFound("No user with that username was found."); 
        }	
        catch(Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
}