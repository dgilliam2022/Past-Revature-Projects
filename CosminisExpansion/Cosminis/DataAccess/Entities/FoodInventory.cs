using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public partial class FoodInventory
    {
        public int UserIdFk { get; set; }
        public int FoodStatsIdFk { get; set; }
        public int? FoodCount { get; set; }

        public virtual FoodStat FoodStatsIdFkNavigation { get; set; } = null!;
        public virtual User UserIdFkNavigation { get; set; } = null!;
    }
}
