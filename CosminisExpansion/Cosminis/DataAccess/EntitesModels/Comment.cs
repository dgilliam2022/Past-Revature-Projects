namespace DataAccess.Entities;

public partial class Comment
    { 
        public override string ToString()
        { 
            return 
                $"CommentId: {this.CommentId}, " + 
                $"UserIdFk: {this.UserIdFk}, " +
                $"PostIdFk: {this.PostIdFk}, " +
                $"Content: {this.Content} "; 
        }  
    }