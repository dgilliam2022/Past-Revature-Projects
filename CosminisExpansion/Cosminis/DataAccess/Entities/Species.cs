using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public partial class Species
    {
        public Species()
        {
            Companions = new HashSet<Companion>();
            Conversations = new HashSet<Conversation>();
        }

        public int SpeciesId { get; set; }
        public int FoodElementIdFk { get; set; }
        public string SpeciesName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int? BaseStr { get; set; }
        public int? BaseDex { get; set; }
        public int? BaseInt { get; set; }
        public int? OpposingEle { get; set; }

        public virtual FoodElement FoodElementIdFkNavigation { get; set; } = null!;
        public virtual FoodElement? OpposingEleNavigation { get; set; }
        public virtual ICollection<Companion> Companions { get; set; }
        public virtual ICollection<Conversation> Conversations { get; set; }
    }
}
