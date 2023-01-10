using System;
using System.Collections.Generic;
using Models;

namespace DataAccess.Entities
{   
    public partial class FoodElement
    {
        public FoodElement(Models.FoodElementWithEnum foodElement)
        {
            FoodStats = new HashSet<FoodStat>();
            //Species = new HashSet<Species>();
            this.FoodElementId = foodElement.FoodElementId;
            this.FoodElement1 = foodElement.FoodElementToString(foodElement.foodElement);
        }

        public override string ToString()
        { 
            return $"FoodElementId: {this.FoodElementId}, FoodElement: {this.FoodElement1}";
        }  
    }
}