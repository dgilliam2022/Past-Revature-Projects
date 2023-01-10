using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;
 
public class CommentRepo : ICommentDAO
{
    private readonly wearelosingsteamContext _context;
    private readonly IResourceGen _resourceRepo;

    public CommentRepo(wearelosingsteamContext context, IResourceGen resourceRepo)
    {
        _context = context;
        _resourceRepo = resourceRepo;
    }

    public Comment SubmitComment(Comment comment)
    {
        try
        {
            _context.Comments.Add(comment); //Add a new comment into the table

            _context.SaveChanges(); //persist the change

            _context.ChangeTracker.Clear(); //clear the tracker for the next comment
        }
        catch(Exception)
        {
            throw;
        }
        return comment; //return the inputed comment info
    }

    public bool AddToPostOwner(Comment comment)
    {
        Post postInfo = _context.Posts.Find(comment.PostIdFk); 
        if(postInfo == null) //such post does not exist
        {
            throw new PostsNotFound();
        }
        User userInfo = _context.Users.Find(postInfo.UserIdFk); //gets the user to the associated userIDfk

        _resourceRepo.AddGold(userInfo, 3);

        return true;
    }

    public List<Comment> GetCommentsByPostId(int postId)
    {
        return _context.Comments.Where(comment => comment.PostIdFk == postId).ToList();
    }

    public bool RemoveComment(int commentId)
    {
        Comment commentInfo = _context.Comments.Find(commentId); 
        if(commentInfo == null) //such comment does not exist
        {
            throw new CommentsNotFound();
        }
        User userInfo = _context.Users.Find(commentInfo.UserIdFk); //gets the user to the associated userIDfk

            _resourceRepo.AddGold(userInfo, -3);
            _context.Comments.Remove(commentInfo); 
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return true;
    }
}