using DataAccess.Entities;
using CustomExceptions;
using Models;
using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;
 
public class CompanionRepo : ICompanionDAO
{
    private readonly wearelosingsteamContext _context;

    public CompanionRepo(wearelosingsteamContext context)
    {
        _context = context;
    }
  
    public Companion GenerateCompanion(int userIdInput)
    {
        Random randomCreature = new Random();
        int creatureRoulette = randomCreature.Next(3,9); 

        Random randomEmotion = new Random();
        int emotionRoulette = randomCreature.Next(0,10); 

        User userInstance = _context.Users.Find(userIdInput);
        if(userInstance == null)
        {
            throw new UserNotFound();
        }
        
        Companion newCompanion = new Companion()
        {
            UserFk = userIdInput,
            SpeciesFk = creatureRoulette,
            Emotion = emotionRoulette,
            Hunger = 100,
            Mood = 75,
            TimeSinceLastChangedMood = DateTime.Now,
            TimeSinceLastChangedHunger = DateTime.Now,
            TimeSinceLastPet = DateTime.Now,
            TimeSinceLastFed = DateTime.Now,
            CompanionBirthday = DateTime.Now
        };

        userInstance.EggCount = userInstance.EggCount - 1;
        if(userInstance.EggCount < 0)
        {
            userInstance.EggCount = 0;
            throw new TooFewResources();
        }
        if(userInstance.EggCount >= 1)
        {
            userInstance.EggTimer = DateTime.Now;
        }

        _context.Companions.Add(newCompanion);

        _context.SaveChanges();

        _context.ChangeTracker.Clear(); 

        return newCompanion;                                                        
    }
    
    public int SetCompanionMood()
    {
        Random randomMood = new Random();
        int companionMood = randomMood.Next(1,4);

        return companionMood;
    }

    public Companion SetCompanionNickname(int companionId, string? nickname)
    {
        Companion selectCompanion = GetCompanionByCompanionId(companionId);
        if(selectCompanion == null)
        {
            throw new CompNotFound();
        }

        selectCompanion.Nickname = nickname;

        _context.SaveChanges();

        _context.ChangeTracker.Clear();

        return selectCompanion;                                
    }

    public List<Companion> GetAllCompanions()
    {
        return _context.Companions.ToList();                                  
    }
   
    public List<Companion> GetCompanionByUser(int userId)
    {
        try
        { 
            List<Companion> companionList = new List<Companion>();

            IEnumerable<Companion> companionQuery =
                from Companions in _context.Companions
                where Companions.UserFk == userId
                select Companions;
        
            foreach(Companion companionReturn in companionQuery)
            {
                companionList.Add(companionReturn);
            }        

            if(companionList.Count() < 1)
            {
                throw new UserNotFound();
            }

            return companionList;
        }
        catch(UserNotFound)
        {
            throw;
        }
    }

    public Companion GetCompanionByCompanionId(int companionId)
    {
            return _context.Companions.FirstOrDefault(companionToBeFound => companionToBeFound.CompanionId == companionId) ?? throw new ResourceNotFound("No companion with this ID exists.");
    }

    public bool DeleteCompanion(int companionId)
    {
        Companion companionToEnd = _context.Companions.Find(companionId);  //Get comp followed by checkifnull
        if(companionToEnd == null)
        {
            throw new CompNotFound();
        }

        User userShowcaseCheck = _context.Users.Find(companionToEnd.UserFk);
        if (userShowcaseCheck.ShowcaseCompanionFk == companionToEnd.CompanionId)
        {
            throw new ShowWontGoYo();
        }

        _context.Companions.Remove(companionToEnd);

        _context.SaveChanges();

        _context.ChangeTracker.Clear();

        return true;
    }
}