using CustomExceptions;
using Models;
using Services;
using DataAccess.Entities;

namespace Controllers;

public class CompanionController
{
    private readonly CompanionServices _service;

    public CompanionController(CompanionServices service)
    {
        _service = service;
    }

    public IResult GetAllCompanions()
    {
        List<Companion> companionList = _service.GetAllCompanions();
        return Results.Accepted("/GetAllCompanions", companionList);
    }

    public IResult SearchForCompanionById(int companionId)
    {
        try
        {   
            Companion queriedCompanion = _service.GetCompanionByCompanionId(companionId);
            return Results.Ok(queriedCompanion);
        }
        catch(CompNotFound)
        {
            return Results.NotFound("There is no companion with this ID");
        }
        catch(ResourceNotFound)
        {
            return Results.NotFound("Something went wrong with this request.");
        }        
    }

    public IResult SearchForCompanionByUserId(int userId)
    {
        try
        {   
            List<Companion> queriedCompanion = _service.GetCompanionByUser(userId);
            return Results.Ok(queriedCompanion);
        }
        catch(UserNotFound)
        {
            return Results.NotFound("There is no user with this ID");
        }
        catch(ResourceNotFound)
        {
            return Results.NotFound("Something went wrong with this request.");
        }         
    }

    public IResult NicknameCompanion(int companionId, string? nickname)
    {
        try
        {   
            Companion companionToName = _service.SetCompanionNickname(companionId, nickname);
            return Results.Created("/companions/Nickname", companionToName);
        }
        catch(ResourceNotFound)
        {
            return Results.NotFound("Invalid Input");
        }
        catch(CompNotFound)
        {
            return Results.BadRequest("There is no companion with this ID");
        }        
    }  

    public IResult GenerateCompanion(string username)
    {
        try
        {   
            int? createdCompanion = _service.HatchCompanion(username);
            return Results.Created("/companions/GenerateCompanion", createdCompanion);
        }
        catch(UserNotFound)
        {
            return Results.NotFound("There is no user with this ID");
        }
        catch(ResourceNotFound)
        {
            return Results.NotFound("Something went wrong with this request.");
        }            
        catch(TooFewResources)
        {
            return Results.Conflict("This user has no eggs");
        }                
        catch(Exception)
        {
            return Results.BadRequest("Invalid Input");
        }
    }

    public IResult DeleteCompanion(int companionId)
    {
        try
        {   
            _service.DeleteCompanion(companionId);
            return Results.Accepted("/Companions/DeleteCompanion");
        }
        catch(CompNotFound)
        {
            return Results.NotFound("No Companion with this ID exists");
        }
        catch(ShowWontGoYo)
        {
            return Results.Conflict("You cannot delete a showcase companion, change your showcase companion first.");
        }         
    }        
}