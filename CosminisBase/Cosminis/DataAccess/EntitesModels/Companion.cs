using System;
using System.Collections.Generic;
using Models;

namespace DataAccess.Entities
{
    public partial class Companion
    { 
        public Companion(Models.CompanionMoodWithEnum Companion)
        {
            this.CompanionId = Companion.CompanionId;
            this.UserFk = Companion.UserFk;
            this.SpeciesFk = Companion.SpeciesFk;
            this.Nickname = Companion.Nickname;
            //this.Mood = Companion.MoodToString(Companion.Mood);
            this.Hunger = Companion.Hunger;
        }

        public override string ToString()
        { 
            return 
                $"CreatureId: {this.CompanionId}, " + 
                $"Nickname: {this.Nickname}, " + 
                //$"Mood: {this.Mood}, " + 
                $"Hunger: {this.Hunger}"; 
        }  
    }
}







   
   
