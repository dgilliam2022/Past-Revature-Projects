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

    public List<Friends> GetAllRelationships()
    {
        return _context.Friends.ToList();
    }

    public List<Friends> ViewAllFriends(int userIdToLookup)
    {
        try
        {
        User userToLookup = _context.Users.Find(userIdToLookup);

        if(userToLookup == null)
        {
            throw new UserNotFound();
        }

        IEnumerable<Friends> friendsQuery =
            (from Friends in _context.Friends
            where (Friends.UserIdTo == userToLookup.UserId) || (Friends.UserIdFrom == userToLookup.UserId)
            select Friends).ToList();

        if(friendsQuery == null)
        {
            throw new RelationshipNotFound();
        }
               
            List<Friends> friendsList = friendsQuery.ToList();

            return friendsList;
        }
        catch(ResourceNotFound)
        {
            throw;
        }                                  
    }
    
    public Friends FriendsByUserIds(int searchingUserId, int user2BeSearchedFor)
    {
        User searchingUser = _context.Users.Find(searchingUserId);
        if(searchingUser == null)
        {
            throw new UserNotFound();
        }
        User friend2BFound = _context.Users.Find(user2BeSearchedFor);
        if(friend2BFound == null)
        {
            throw new UserNotFound();
        }
        List<Friends> quieriedFriendsList = ViewAllFriends((int)searchingUser.UserId);

        if(quieriedFriendsList == null)
        {
            throw new RelationshipNotFound();
        }

        List<Friends> searchedFriendsList = ViewAllFriends((int)friend2BFound.UserId);

        if(searchedFriendsList == null)
        {
            throw new RelationshipNotFound();
        }

        Friends returnRelationship = new Friends();

        foreach(Friends friendInstance in quieriedFriendsList)
        {
            if(searchedFriendsList.Contains(friendInstance))
            {
                returnRelationship = friendInstance;
            }
        }
        return returnRelationship;
    }
    
    public Friends EditFriendship(int editingUserID, int user2BeEdited, string status)//this does not delete removed/blocked friends. I may add this later.
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
        
        Friends statusToBeEdited = FriendsByUserIds((int)editingUser.UserId, (int)friend2BeEdited.UserId);

        statusToBeEdited.Status = status;

        _context.SaveChanges();

        _context.ChangeTracker.Clear(); 

        return statusToBeEdited;  
    }

    public Friends SearchByRelationshipId(int relationshipId)
    {
        return _context.Friends.FirstOrDefault(friends => friends.RelationshipId == relationshipId) ?? throw new ResourceNotFound();
    }

    public List<Friends> CheckRelationshipStatusByUsername(string username, string status)
    {   
        User quieriedUser = _userRepo.GetUserByUserName(username);

        List<Friends> quieriedUsersList = ViewAllFriends((int)quieriedUser.UserId);

        List<Friends> statusList = new List<Friends>();

        if(quieriedUsersList == null)
        {
            throw new ResourceNotFound();
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
            throw new ResourceNotFound();
        }

        return statusList;
    }

    public List<Friends> CheckRelationshipStatusByUserId(int searchingId, string status)
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
    public List<Friends> ViewRelationShipsByStatus(string status)
    {
        List<Friends> relationsList = new List<Friends>();

        switch(status) 
        {
        case "Pending":
            try
            {
            IEnumerable<Friends> statusQuery =
                from Friends in _context.Friends
                where Friends.Status == RelationshipStatus.Pending.ToString()
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
        case "Accepted":
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
        case "Removed":
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
        case "Blocked":
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
        default:
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

        return relationsList;                  
    }

    public Friends AddFriendByUserId(int userToAddId, int acceptingUserId)
    {
        User toBeAccepted = _context.Users.Find(userToAddId);
        User requestReceiver = _context.Users.Find(acceptingUserId);

        if(toBeAccepted == null || requestReceiver == null)
        {
            throw new UserNotFound();
        }

        Friends friendInstance = FriendsByUserIds((int)requestReceiver.UserId, (int)toBeAccepted.UserId);

        Console.WriteLine(friendInstance);

        if(friendInstance.Status != null)
        {
            Console.WriteLine(friendInstance.Status);
            if(friendInstance.Status == "Accepted")
            {
                Console.WriteLine("A");               //These comments for testing
                throw new DuplicateFriends();
            }     
            else if(friendInstance.Status == "Blocked")
            {
                Console.WriteLine("B");
                throw new BlockedUser();
            }
            else if(friendInstance.Status == "Pending")
            {
                Console.WriteLine("P");
                throw new PendingFriends();
            }
            else if(friendInstance.Status == "Removed")
            {
                Console.WriteLine("R");
                friendInstance.Status = "Pending";

                _context.SaveChanges();

                _context.ChangeTracker.Clear();

                return friendInstance;                    
            }                
        }
        else if(friendInstance.Status == null)
        {
            Console.WriteLine("N");
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

    public Friends AddFriendByUsername(string userToAdd, string acceptingUser)
    {
        User toBeAccepted = _userRepo.GetUserByUserName(userToAdd);               //searching friends
        User requestReceiver = _userRepo.GetUserByUserName(acceptingUser);

        if(toBeAccepted == null || requestReceiver == null)                               //checking null
        {           
            throw new UserNotFound();
        }
        
        Friends friendInstance = FriendsByUserIds((int)requestReceiver.UserId, (int)toBeAccepted.UserId);

        Console.WriteLine(friendInstance);

        if(friendInstance.Status != null)
        {
            Console.WriteLine(friendInstance.Status);
            if(friendInstance.Status == "Accepted")
            {
                Console.WriteLine("A");               //These comments for testing
                throw new DuplicateFriends();
            }     
            else if(friendInstance.Status == "Blocked")
            {
                Console.WriteLine("B");
                throw new BlockedUser();
            }
            else if(friendInstance.Status == "Pending")
            {
                Console.WriteLine("P");
                throw new PendingFriends();
            }
            else if(friendInstance.Status == "Removed")
            {
                Console.WriteLine("R");
                friendInstance.Status = "Pending";

                _context.SaveChanges();

                _context.ChangeTracker.Clear();

                return friendInstance;                    
            }                
        }
        else if(friendInstance.Status == null)
        {
            Console.WriteLine("N");
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