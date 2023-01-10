using Services;
using Models;
using CustomExceptions;

namespace WebAPI.Controllers;

public class AuthController
{
	private readonly AuthService _service;

    public AuthController(AuthService service) {
        _service = service;
    }
    
    public IResult Login(string username, string password) {
        try 
        {
            Users userinfo = _service.Login(username, password);
            return Results.Ok(userinfo); 
        }
        catch(InvalidCredentials)
        {
            return Results.Unauthorized();
        }
        catch(ResourceNotFound)
        {
            return Results.BadRequest("Username cannot be found");
        }
    }

    public IResult RegisterUser(Users userToRegister) {
        if (String.IsNullOrWhiteSpace(userToRegister.userName))
        {
            return Results.BadRequest("Username cannot be empty");
        }
        try
        {
            _service.RegisterUser(userToRegister);
            return Results.Created("/register", userToRegister);
        }
        catch (UsernameNotAvailable)
        {
            return Results.Conflict("User with this username already exists");
        }
    }

}