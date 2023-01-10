using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public partial class EmotionChart
    {
        public EmotionChart()
        {
            Companions = new HashSet<Companion>();
        }

        public int EmotionId { get; set; }
        public int Quality { get; set; }
        public string Emotion { get; set; } = null!;

        public virtual ICollection<Companion> Companions { get; set; }
    }
}
