using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccess;
 
public class FriendsRepo : IFriendsDAO
{
    private readonly wearelosingsteamContext _context;
    private readonly IUserDAO _userRepo;

    public FriendsRepo(wearelosingsteamContext context, IUserDAO userRepo)
    {
        _context = context;
        _userRepo = userRepo;
    }

    public List<Friends> GetAllRelationships()                          //Getting EVERY friendship in the database. I mostly made this for practice at the time.
    {
        return _context.Friends.ToList();
    }

    public List<Friends> ViewAllFriends(int userIdToLookup)             //More useful... gets ALLLL "friends" ('relationships' to be specific) of a particular user
    {
        try
        {
        User userToLookup = _context.Users.Find(userIdToLookup);

        if(userToLookup == null)
        {
            throw new UserNotFound();
        }

        IEnumerable<Friends> friendsQuery =                             //This will return friends EVEN IF THEY ARE REMOVED OR BLOCKED BTW... one for only 'accepted' friends below.
            (from Friends in _context.Friends
            where (Friends.UserIdTo == userToLookup.UserId) || (Friends.UserIdFrom == userToLookup.UserId)
            select Friends).ToList();

        if(friendsQuery == null)                                        //Throws an exception if the user is unpopular.
        {
            throw new RelationshipNotFound();
        }
               
        List<Friends> friendsList = friendsQuery.ToList();              //...Otherwise it adds what it finds to a list to return in two lines.

        return friendsList;
        }
        catch(ResourceNotFound)                                         //Another likely unreachable catch statement.
        {
            throw;                                                     
        }                                  
    }
    
    public Friends FriendsByUserIds(int searchingUserId, int user2BeSearchedFor)//Pretty useful. Finds relationships between two specific people if it exists
    {
        User searchingUser = _context.Users.Find(searchingUserId);              //lets look for two users and return null if null
        if(searchingUser == null)
        {
            throw new UserNotFound();
        }
        User friend2BFound = _context.Users.Find(user2BeSearchedFor);
        if(friend2BFound == null)
        {
            throw new UserNotFound();
        }
        List<Friends> quieriedFriendsList = ViewAllFriends((int)searchingUser.UserId);//Getting all friends by user 1

        if(quieriedFriendsList == null)
        {
            throw new RelationshipNotFound();
        }

        List<Friends> searchedFriendsList = ViewAllFriends((int)friend2BFound.UserId);//Getting all friends by user 2

        if(searchedFriendsList == null)             
        {
            throw new RelationshipNotFound();
        }

        Friends returnRelationship = new Friends();

        foreach(Friends friendInstance in quieriedFriendsList)                      //Looking through the second friendship
        {
            if(searchedFriendsList.Contains(friendInstance))                        //if it contains the first one then we got a match
            {
                returnRelationship = friendInstance;                            
            }
        }
        return returnRelationship;                  //I kinda use friendship and relationship interchangeably FYI. This returns the searched relationship.
    }
    
    public Friends EditFriendship(int editingUserID, int user2BeEdited, string status)//Edits friendships(relationship) 'status' "Accepted, Pending, Removed, Blocked."
    {   
        User editingUser = _context.Users.Find(editingUserID);
        if(editingUser == null)
        {
            throw new UserNotFound();
        }
        
        User friend2BeEdited = _context.Users.Find(user2BeEdited);
        if(friend2BeEdited == null)
        {
            throw new UserNotFound();
        }
        
        Friends statusToBeEdited = FriendsByUserIds((int)editingUser.UserId, (int)friend2BeEdited.UserId);//Grabs a relationship between two users

        statusToBeEdited.Status = status;                                   //changes the status to the input status

        _context.SaveChanges();

        _context.ChangeTracker.Clear(); 

        return statusToBeEdited;                                            //returns the edited relationship
    }

    public Friends SearchByRelationshipId(int relationshipId)           //Searching for a specific relationship by the relationship Id, and returning it
    {
        return _context.Friends.FirstOrDefault(friends => friends.RelationshipId == relationshipId) ?? throw new ResourceNotFound();
    }

    public List<Friends> CheckRelationshipStatusByUsername(string username, string status)//looks for all relationships of a specific status for 1 user. (All accepted, all pending....)
    {   
        User quieriedUser = _userRepo.GetUserByUserName(username);

        List<Friends> quieriedUsersList = ViewAllFriends((int)quieriedUser.UserId);//Gets a list of all the current friends of selected user

        List<Friends> statusList = new List<Friends>();                            //This will be for the return

        if(quieriedUsersList == null)
        {
            throw new ResourceNotFound();
        }
        foreach(Friends friendSearch in quieriedUsersList)                         //Go through the queried list...
        {
            if(friendSearch.Status.ToString() == status)                           //Filter the results by status....
            {
                statusList.Add(friendSearch);                                      //Add the relationships to the final status list to return
            }
        }
        if(statusList.Count() < 1)
        {
            throw new ResourceNotFound();
        }

        return statusList;                      
    }

    public List<Friends> CheckRelationshipStatusByUserId(int searchingId, string status)//Same thing as above but by userId instead of username
    {   
        User quieriedUser = _context.Users.Find(searchingId);                       
        if(quieriedUser == null)                                                    
        {
            throw new UserNotFound();
        }

        List<Friends> quieriedUsersList = ViewAllFriends((int)quieriedUser.UserId); 

        List<Friends> statusList = new List<Friends>();                             

        if(quieriedUsersList == null)
        {
            throw new UserNotFound();
        }
        foreach(Friends friendSearch in quieriedUsersList)
        {
            if(friendSearch.Status.ToString() == status)
            {
                statusList.Add(friendSearch);
            }
        }
        if(statusList.Count() < 1)
        {
            throw new RelationshipNotFound();
        }

        return statusList;
    }    
    /* More methods to come for more specificity? -Get relationship by usernames and status, userID's and status, and then one by two usernames...
    public Friends CheckRelationshipStatusByUserId(int searchingId, int friendsId, string status)
    {   
        User quieriedUser = _context.Users.Find(searchingId);
        User friendOfUser = _context.Users.Find(friendsId);

        List<Friends> quieriedUsersList = ViewAllFriends(quieriedUser.UserId);

        if(quieriedUsersList == null)
        {
            throw new ResourceNotFound();
        }
        else
        {
            foreach(Friends friendSearch in quieriedUsersList)
            {
                if(friendSearch.UserIdFrom == friend2BeAdded.UserId || friendSearch.UserIdTo == friend2BeAdded.UserId)
                {
                    Friends friendReturn = ViewRelationShipsByStatus(status);
                    return friendReturn;
                }
            }
        }
    }
    */
    public List<Friends> ViewRelationShipsByStatus(string status)//Gets all relationships in the database of a particular status
    {
        List<Friends> relationsList = new List<Friends>();       //List of relationships to return

        switch(status)                                           
        {                                                        //Using a switch based on entered.
        case "Pending":                                          //Searching based on the 'Pending' relationship status.
            try
            {
            IEnumerable<Friends> statusQuery =                   
                from Friends in _context.Friends                 //(Logic the same in below cases)
                where Friends.Status == RelationshipStatus.Pending.ToString()
                select Friends;

            foreach(Friends friendsReturn in statusQuery)        //Iterates through the query and adds the appropriate relationships to a list.
            {                                                       
                relationsList.Add(friendsReturn);
            }  

            if(relationsList.Count() < 1)                        //If the list is empty it throws an exception.
            {
                throw new RelationshipNotFound();
            }

            return relationsList;                                //Returns the list.
            }
            catch(ResourceNotFound)                         
            {
                throw;
            }
            break;
        case "Accepted":                                         //Searching based on the 'Accepted' relationship status.
            try
            {
            IEnumerable<Friends> statusQuery =
                from Friends in _context.Friends
                where Friends.Status == RelationshipStatus.Accepted.ToString()
                select Friends;

            foreach(Friends friendsReturn in statusQuery)
            {
                relationsList.Add(friendsReturn);
            }  

            if(relationsList.Count() < 1)
            {
                throw new RelationshipNotFound();
            }

            return relationsList;
            }
            catch(ResourceNotFound)
            {
                throw;
            }
            break;
        case "Removed":                                          //Searching based on the 'Removed' relationship status.
            try
            {
            IEnumerable<Friends> statusQuery =
                from Friends in _context.Friends
                where Friends.Status == RelationshipStatus.Removed.ToString()
                select Friends;

            foreach(Friends friendsReturn in statusQuery)
            {
                relationsList.Add(friendsReturn);
            }  

            if(relationsList.Count() < 1)
            {
                throw new RelationshipNotFound();
            }

            return relationsList;
            }
            catch(ResourceNotFound)
            {
                throw;
            }
            break;
        case "Blocked":                                          //Searching based on the 'Blocked' relationship status.
            try
            {
            IEnumerable<Friends> statusQuery =
                from Friends in _context.Friends
                where Friends.Status == RelationshipStatus.Blocked.ToString()
                select Friends;

            foreach(Friends friendsReturn in statusQuery)
            {
                relationsList.Add(friendsReturn);
            }  

            if(relationsList.Count() < 1)
            {
                throw new RelationshipNotFound();
            }

            return relationsList;
            }
            catch(ResourceNotFound)
            {
                throw;
            }
            break;    
        default:                                                 //Default condition searches based on the 'Accepted' relationship status.
            try
            {
            IEnumerable<Friends> statusQuery =
                from Friends in _context.Friends
                where Friends.Status == RelationshipStatus.Accepted.ToString()
                select Friends;

            foreach(Friends friendsReturn in statusQuery)
            {
                relationsList.Add(friendsReturn);
            }  

            if(relationsList.Count() < 1)
            {
                throw new Exception("There are no relationships with this status.");
            }

            return relationsList;
            }
            catch(Exception E)
            {
                throw;
            }
            break;
        };

        return relationsList;                                    //Returns relationslist
    }

    public Friends AddFriendByUserId(int userToAddId, int acceptingUserId)//Adds a friend/creates a relationship via user Id. Next method is the same, but by username.
    {
        User toBeAccepted = _context.Users.Find(userToAddId);             //The user who is getting a friend request
        User requestReceiver = _context.Users.Find(acceptingUserId);      //The user sending the friend request

        if(toBeAccepted == null || requestReceiver == null)               //If either user doesn't exists, returns null
        {
            throw new UserNotFound();
        }

        Friends friendInstance = FriendsByUserIds((int)requestReceiver.UserId, (int)toBeAccepted.UserId);//Searching to see if these users are in a friend relationship yet

        if(friendInstance.Status != null)                       //If they are ALREADY FRIENDS(in a 'relationship'), this will check what their status is
        {
            if(friendInstance.Status == "Accepted")             //If 'Accepted' it will say "you are friends already, yo."
            {
                throw new DuplicateFriends();
            }     
            else if(friendInstance.Status == "Blocked")         //We don't even give them information if it is a blocked relationship. "There is no information."
            {
                throw new BlockedUser();
            }
            else if(friendInstance.Status == "Pending")         //If they are in a 'Pending' relationship, it will return "This relationship is already pending."
            {
                throw new PendingFriends();
            }
            else if(friendInstance.Status == "Removed")         //If they are simply removed, then you can still send a new request
            {
                friendInstance.Status = "Pending";              //The request becomes 'pending' again if it is true.

                _context.SaveChanges();

                _context.ChangeTracker.Clear();

                return friendInstance;                    
            }                
        }
        else if(friendInstance.Status == null)                  //If the initial relationship search is non existent, it creates a new pending relationship!
        {
            Friends newRelationship = new Friends
            {
                UserIdFrom = (int)toBeAccepted.UserId,
                UserIdTo = (int)requestReceiver.UserId,
                Status = "Pending"
            };

            _context.Friends.Add(newRelationship);

            _context.SaveChanges();                             //Saves the new relationship.

            _context.ChangeTracker.Clear();

            return newRelationship;                
        }            
                   
        return friendInstance;                                  //Returns the relationship instance if applicable.
    }

    public Friends AddFriendByUsername(string userToAdd, string acceptingUser)//Same as above but by username
    {
        User toBeAccepted = _userRepo.GetUserByUserName(userToAdd);               
        User requestReceiver = _userRepo.GetUserByUserName(acceptingUser);

        if(toBeAccepted == null || requestReceiver == null)                       
        {           
            throw new UserNotFound();
        }
        
        Friends friendInstance = FriendsByUserIds((int)requestReceiver.UserId, (int)toBeAccepted.UserId);

        if(friendInstance.Status != null)
        {
            if(friendInstance.Status == "Accepted")
            {
                throw new DuplicateFriends();
            }     
            else if(friendInstance.Status == "Blocked")
            {
                throw new BlockedUser();
            }
            else if(friendInstance.Status == "Pending")
            {
                throw new PendingFriends();
            }
            else if(friendInstance.Status == "Removed")
            {
                friendInstance.Status = "Pending";

                _context.SaveChanges();

                _context.ChangeTracker.Clear();

                return friendInstance;                    
            }                
        }
        else if(friendInstance.Status == null)
        {
            Friends newRelationship = new Friends
            {
                UserIdFrom = (int)toBeAccepted.UserId,
                UserIdTo = (int)requestReceiver.UserId,
                Status = "Pending"
            };

            _context.Friends.Add(newRelationship);

            _context.SaveChanges();

            _context.ChangeTracker.Clear();

            return newRelationship;                
        }            
                   
        return friendInstance;
    }  
}