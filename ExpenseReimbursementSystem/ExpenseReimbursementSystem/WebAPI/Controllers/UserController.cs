using Services;
using Models;
using CustomExceptions;

namespace WebAPI.Controllers;

public class UserController
{
	private readonly UserService _service;

    public UserController(UserService service) {
        _service = service;
    }

    public List<Users> GetAllUsers()  {
        return _service.GetAllUsers();
    }

    public IResult GetByUsername(string username) {
        Users userinfo = _service.GetByUsername(username);
        if (String.IsNullOrWhiteSpace(userinfo.userName))
        {
            return Results.BadRequest("Username cannot be found");
        }
        try 
        {  
            return Results.Ok(userinfo); 
        }
        catch(UsernameNotAvailable)
        {
            return Results.BadRequest("Username cannot be found");
        }
    }

     public IResult GetByUserID(int ID) {
        Users userinfo = _service.GetByUserID(ID);
        if (userinfo.userID == 0)
        {
            return Results.BadRequest("User ID cannot be found");
        }
        try 
        {  
            return Results.Ok(userinfo); 
        }
        catch(UsernameNotAvailable)
        {
            return Results.BadRequest("User ID cannot be found");
        }
    }
}