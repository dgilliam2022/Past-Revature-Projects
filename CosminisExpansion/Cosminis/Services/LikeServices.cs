using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class LikeServices
{
    private readonly ILikeIt _LikeRepo;

    public LikeServices(ILikeIt LikeRepo)
    {
        _LikeRepo = LikeRepo;
    }

    public bool RemoveLikes(int UserID, int PostID)
    {
        try
        {
            return _LikeRepo.RemoveLikes(UserID, PostID);
        }
        catch(Exception)
        {
            throw;
        }
        return false;
    }

    public bool AddLikes(int UserID, int PostID)
    {
        try
        {
            return _LikeRepo.AddLikes(UserID, PostID);
        }
        catch(Exception)
        {
            throw;
        }
        return false;
    }

    public int LikeCount(int PostID)
    {
        try
        {
            return _LikeRepo.LikeCount(PostID);
        }
        catch(Exception)
        {
            throw;
        }
    }
}