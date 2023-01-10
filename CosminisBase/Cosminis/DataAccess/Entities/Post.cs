using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            UserIdFks = new HashSet<User>();
        }

        public int PostId { get; set; }
        public int UserIdFk { get; set; }
        public string Content { get; set; } = null!;

        public virtual User UserIdFkNavigation { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<User> UserIdFks { get; set; }
    }
}
