using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Controllers;

public class PostController
{
	private readonly PostServices _postServices;

    public PostController(PostServices postServices)
    {
        _postServices = postServices;
    }

    public IResult SubmitPostResourceGen(string Content, int PosterID)
    { 
        if (Content.Length > 600) 
        {
            return Results.Conflict("Posts' content cannot be greater than 600 characters."); //processing the request would create a conflict within the resource
        }
        try
        {
            Post postInfo = _postServices.SubmitPostResourceGen(Content, PosterID);
            return Results.Created("/submitPost", postInfo);   
        }
        catch(ResourceNotFound)
        {
            return Results.NotFound("Such a user does not exist"); 
        }   
    }

    public IResult GetPostsByUserId(int userId)
    {
        try 
        {
            List<Post> posts = _postServices.GetPostsByUserId(userId);
            return Results.Ok(posts); 
        }
        catch(PostsNotFound)
        {
            return Results.NotFound("There are no posts associated with that user ID"); 
        }
    }

    public IResult GetPostsByUsername(string username)
    {
        try 
        {
            List<Post> posts = _postServices.GetPostsByUsername(username);
            return Results.Ok(posts); 
        }
        catch(PostsNotFound)
        {
            return Results.NotFound("There are no posts associated with that username"); 
        }  
        catch(ResourceNotFound)
        {
            return Results.NotFound("No user with that username was found."); 
        }
    }

    public IResult GetAllFriendsPosts(string username)
    {
        try
        {
            List<Post> friendsPosts = _postServices.GetAllFriendsPosts(username);
            return Results.Ok(friendsPosts); 
        }
        catch(RelationshipNotFound)
        {
            return Results.NotFound("That user has no friends.");    
        }
    }
}