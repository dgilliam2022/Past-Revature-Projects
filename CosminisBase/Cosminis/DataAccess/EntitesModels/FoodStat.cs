using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public partial class FoodStat
    {
        public override string ToString()
        {
            return
                $"FoodStatsId: {this.FoodStatsId}, " +
                $"Description: {this.Description}, " + 
                $"FoodName: {this.FoodName}, " + 
                $"HungerRestore: {this.HungerRestore}, ";
        }
            
    }
}