using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;
 
public class PostRepo : IPostDAO
{
    private readonly wearelosingsteamContext _context;
    private readonly IUserDAO _userRepo;

    public PostRepo(wearelosingsteamContext context, IUserDAO userRepo)
    {
        _context = context;
        _userRepo = userRepo;
    }

    public Post SubmitPost(Post post)
    {
        _context.Posts.Add(post); //Add a new post into the table

        _context.SaveChanges(); //persist the change

        _context.ChangeTracker.Clear(); //clear the tracker for the next post

        return post; //return the inputed post info
    }

    public List<Post> GetPostsByUserId(int? userId)
    {
        return _context.Posts.Where(post => post.UserIdFk == userId).ToList();
    }

    public List<Post> GetPostsByUsername(string username)
    {
        User userInfo = _userRepo.GetUserByUserName(username);
        //return _context.Posts.Where(post => post.UserIdFk == userInfo.UserId).ToList();
        return GetPostsByUserId(userInfo.UserId);
    } 

}