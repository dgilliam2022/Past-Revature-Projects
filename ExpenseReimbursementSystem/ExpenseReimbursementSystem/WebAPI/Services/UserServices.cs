using System.Data.SqlClient;
using DAO;
using Models;
using System.Collections.Generic;
using CustomExceptions;

namespace Services;

public class UserService
{
    UsersDAO useraccess = new UsersDAO();

    public List<Users> GetAllUsers()  {
    	return useraccess.GetAllUsers();
    }

    public Users GetByUsername(string username) {
        try 
        {
            return useraccess.GetByUsername(username);
        }
        catch(UsernameNotAvailable)
        {
            throw new UsernameNotAvailable();
        }

    }

    public Users GetByUserID(int ID) {
        try 
        {
            return useraccess.GetByUserID(ID);
        }
        catch(UsernameNotAvailable)
        {
            throw new UsernameNotAvailable();
        }
    }
}