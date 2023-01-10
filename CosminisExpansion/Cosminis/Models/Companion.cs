using System;
using System.Collections.Generic;
using CustomExceptions;

namespace Models;

public enum MoodCompanion                            
{
    Happy, Sad, Angry, Tired, Anxious, Excited, Chill
}

public class CompanionMoodWithEnum
{ 
    public int CompanionId { get; set; }
    public int UserFk { get; set; }
    public int SpeciesFk { get; set; }
    public string? Nickname { get; set; }
    public MoodCompanion Mood { get; set; }
    public int? Hunger { get; set; }
    
    public string MoodToString(MoodCompanion Mood)                                   
    {
        Dictionary<MoodCompanion, string> dictMood = new Dictionary<MoodCompanion, string>()
        {
            {MoodCompanion.Happy, "Happy"},
            {MoodCompanion.Sad, "Sad"},
            {MoodCompanion.Angry, "Angry"},
            {MoodCompanion.Tired, "Tired"},
            {MoodCompanion.Anxious, "Anxious"},
            {MoodCompanion.Excited, "Excited"},
            {MoodCompanion.Chill, "Chill"}

        };

        if(dictMood.ContainsKey(Mood))
        {
            return dictMood[Mood];
        }
        throw new MoodNotFound();
    }

    public override string ToString()
    { 
        return 
            $"CreatureId: {this.CompanionId}, " + 
            $"Nickname: {this.Nickname}, " + 
            $"Mood: {this.Mood}, " + 
            $"Hunger: {this.Hunger}"; 
    }  
}