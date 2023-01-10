using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public partial class FoodElement
    {
        public FoodElement()
        {
            FoodStats = new HashSet<FoodStat>();
            SpeciesFoodElementIdFkNavigations = new HashSet<Species>();
            SpeciesOpposingEleNavigations = new HashSet<Species>();
        }

        public int FoodElementId { get; set; }
        public string FoodElement1 { get; set; } = null!;

        public virtual ICollection<FoodStat> FoodStats { get; set; }
        public virtual ICollection<Species> SpeciesFoodElementIdFkNavigations { get; set; }
        public virtual ICollection<Species> SpeciesOpposingEleNavigations { get; set; }
    }
}
