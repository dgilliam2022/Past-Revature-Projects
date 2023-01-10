using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public partial class Conversation
    {
        public int ConversationId { get; set; }
        public int SpeciesFk { get; set; }
        public int Quality { get; set; }
        public string Message { get; set; } = null!;

        public virtual Species SpeciesFkNavigation { get; set; } = null!;
    }
}
