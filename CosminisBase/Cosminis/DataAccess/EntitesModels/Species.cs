using System;
using System.Collections.Generic;
using Models;

namespace DataAccess.Entities
{
    public partial class Species
    { 
        public Species(Models.SpeciesWithEnum Species)
        {
            Companions = new HashSet<Companion>();
            this.BaseDex = Species.BaseDex;
            this.BaseInt = Species.BaseInt;
            this.BaseStr = Species.BaseStr;
            this.Description = Species.Description;
            this.SpeciesId = Species.SpeciesId;
            this.SpeciesName = Species.SpeciesName;
            this.FoodElementIdFk = Species.FoodElementIdFk;
        }

        public override string ToString()
        { 
            return 
                $"SpeciesId: {this.SpeciesId}, " + 
                $"SpeciesName: {this.SpeciesName}, " + 
                $"Description: {this.Description}, " + 
                $"BaseStr: {this.BaseStr}, " + 
                $"BaseDex: {this.BaseDex}, " + 
                $"BaseInt: {this.BaseInt}";
        }  
    }
}
