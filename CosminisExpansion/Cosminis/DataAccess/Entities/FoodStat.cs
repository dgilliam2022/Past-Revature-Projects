using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public partial class FoodStat
    {
        public FoodStat()
        {
            FoodInventories = new HashSet<FoodInventory>();
        }

        public int FoodStatsId { get; set; }
        public int FoodElementFk { get; set; }
        public string? Description { get; set; }
        public string FoodName { get; set; } = null!;
        public int HungerRestore { get; set; }

        public virtual FoodElement FoodElementFkNavigation { get; set; } = null!;
        public virtual ICollection<FoodInventory> FoodInventories { get; set; }
    }
}
