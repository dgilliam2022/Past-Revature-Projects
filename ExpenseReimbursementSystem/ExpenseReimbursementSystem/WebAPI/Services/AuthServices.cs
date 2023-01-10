using Models;
using CustomExceptions;
using DAO;

namespace Services;

public class AuthService
{
    UsersDAO useraccess = new UsersDAO();

    public Users Login(string username, string password)
    {
        try
        {
           Users user = useraccess.GetByUsername(username); 
            if (String.IsNullOrWhiteSpace(user.userName))
            {
                throw new ResourceNotFound();
            }
            else if (user.password == password)
            {
                return user;
            }
            else 
            { 
                throw new InvalidCredentials(); 
            }
        }
        catch (ResourceNotFound)
        {
            throw new ResourceNotFound();
        }
        catch (InvalidCredentials)
        {
            throw new InvalidCredentials();
        }
    }
    public Users RegisterUser(Users reguser)
    {
        try
        {
            Users newUser = useraccess.GetByUsername(reguser.userName);
            if(newUser.userName == reguser.userName)
            {
                throw new UsernameNotAvailable();
            }
            else
            {
                Users user = useraccess.CreateUser(reguser);
                return user;
            }
        }
            
            catch(UsernameNotAvailable)
            {
                throw new UsernameNotAvailable();                
            }
    }
}
        