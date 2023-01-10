using DataAccess;
using CustomExceptions;
using Models;
using DataAccess.Entities;

namespace Services;

public class CompanionServices
{
    private readonly ICompanionDAO _CompanionRepo;
    
	private readonly IUserDAO _userRepo;

    public CompanionServices(ICompanionDAO CompanionRepo, IUserDAO userRepo)
    {
        _CompanionRepo = CompanionRepo;
        _userRepo = userRepo;
    }

    public int? HatchCompanion(string username)
    {
        try
        {
            User newUser = _userRepo.GetUserByUserName(username);
            if(newUser == null)
            {
                throw new UserNotFound();
            }
            Companion companionToGenerate = _CompanionRepo.GenerateCompanion((int)newUser.UserId);


            return companionToGenerate.CompanionId;

        }
        catch (ResourceNotFound)
        {
            throw;
        }
    }

    public Companion SetCompanionNickname(int companionId, string? nickname)
    {
        try
        {
            Companion checkCompanion = _CompanionRepo.GetCompanionByCompanionId(companionId);
            if(checkCompanion == null)
            {
                throw new ResourceNotFound();
            }
            return _CompanionRepo.SetCompanionNickname(companionId, nickname);
        }
        catch (CompNotFound)
        {
            throw;
        }
    }

    public List<Companion> GetAllCompanions()
    {
        return _CompanionRepo.GetAllCompanions();
    }

    public List<Companion> GetCompanionByUser(int userId)
    {
        try
        {
            List<Companion> checkUser = _CompanionRepo.GetCompanionByUser(userId);
            if(checkUser == null)
            {
                throw new UserNotFound();
            }
            return _CompanionRepo.GetCompanionByUser(userId);
        }
        catch (ResourceNotFound)
        {
            throw;
        }
    }

    public Companion GetCompanionByCompanionId(int companionId)
    {
        try
        {
            Companion checkCompanion = _CompanionRepo.GetCompanionByCompanionId(companionId);
            if(checkCompanion == null)
            {
                throw new CompNotFound();
            }
            return _CompanionRepo.GetCompanionByCompanionId(companionId);
        }
        catch (ResourceNotFound)
        {
            throw;
        }
    }

    public bool DeleteCompanion(int companionId)
    {
        try
        {
            _CompanionRepo.DeleteCompanion(companionId);
            if(companionId == null)
            {
                throw new UserNotFound();
            }
            return _CompanionRepo.DeleteCompanion(companionId);
        }
        catch (CompNotFound)
        {
            throw;
        }        

        return false;
    }            
}