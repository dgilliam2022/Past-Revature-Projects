using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class CommentServices
{
	private readonly ICommentDAO _commentRepo;
	private readonly IResourceGen _resourceRepo;

    public CommentServices(ICommentDAO commentRepo, IResourceGen resourceRepo)
    {
        _commentRepo = commentRepo;
        _resourceRepo = resourceRepo;
    }

    public Comment SubmitCommentResourceGen(int commenterID, int postsID, string content)
    {
        Comment newComment = new Comment()
        {
            UserIdFk = commenterID,
            PostIdFk = postsID,
            Content = content
        };

    	User shellUser = new User();
    	shellUser.UserId = newComment.UserIdFk; //this sets the shellUser's id to the comment's useridkfk, now shellUser actually has some useful info (a user ID) 

    	int goldToAdd = 0;
    	int charCount = newComment.Content.Length;  //gets the length of each comment's content

        if (charCount <= 10)
        {
        	goldToAdd = 0;
        }
        else if (charCount > 10 && charCount <= 80)
        {
        	goldToAdd = 3;
        }
    	else if (charCount > 80)
    	{
    		goldToAdd = 6;
    	}
 	
        _resourceRepo.AddGold(shellUser, goldToAdd);
        _commentRepo.AddToPostOwner(newComment);
        try
        {
            return _commentRepo.SubmitComment(newComment);
        }
        catch(Exception)
        {
            throw;
        }
    }

    public List<Comment> GetCommentsByPostId(int postId)
    {
        List<Comment> comments = _commentRepo.GetCommentsByPostId(postId);
        if (comments.Count < 1) 
        {
            throw new CommentsNotFound();
        }
        return comments; 
    }


    public bool RemoveComment(int commentId)
    {
        try
        {
            return _commentRepo.RemoveComment(commentId);
        }
        catch(CommentsNotFound)
        {
            throw;
        }
        return false;
    }
}