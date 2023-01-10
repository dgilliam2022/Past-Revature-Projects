namespace Models;

public class Users
{
    public int userID { get; set; }
    public string userName { get; set; }
    public string password { get; set; } 
    public string role { get; set; }

    public Users() {
   
    }

    public Users(string userName, string password, string role) {
        this.userName = userName;
        this.password = password;
        this.role = role;
    }

    public Users(int userID, string userName, string password, string role) {
        this.userID = userID;
        this.userName = userName;
        this.password = password;
        this.role = role;
    }

    public override string ToString() {
        return "User ID: " + this.userID +
        ", Username: " + this.userName +
        ", Password: " + this.password +
        ", Role: " + this.role;
    }
}