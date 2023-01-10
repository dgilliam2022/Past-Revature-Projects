namespace DataAccess.Entities;

public partial class Post
    { 
        public override string ToString()
        { 
            return 
                $"PostId: {this.PostId}, " + 
                $"Content: {this.Content} "; 
        }  
    }
