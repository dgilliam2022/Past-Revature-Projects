using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int UserIdFk { get; set; }
        public int PostIdFk { get; set; }
        public string Content { get; set; } = null!;

        public virtual Post PostIdFkNavigation { get; set; } = null!;
        public virtual User UserIdFkNavigation { get; set; } = null!;
    }
}
