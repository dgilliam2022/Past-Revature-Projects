using Services;
using DAO;
using Models;
using WebAPI.Controllers;

var builder = WebApplication.CreateBuilder(args);

//builder.Configuration.GetConnectionString("ERS_DB");
//depedency injection container
builder.Services.AddScoped<UserController>();
builder.Services.AddScoped<TicketController>();
builder.Services.AddScoped<AuthController>(); //im assuming we need to create a txt file for our WebAPI documentation? doesn't seem like it
builder.Services.AddScoped<UserService>(); //is it not weird that I have to have service and controller as dependency? technically you should need more
builder.Services.AddScoped<AuthService>(); //do we need to deploy this to the web through azure? optional
builder.Services.AddScoped<TicketService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello, welcome to the Expense Reinbursement System API!");

app.MapGet("/users", (UserController controller) => //this is not very clear on trello?? answered but not working
{
	return controller.GetAllUsers();
});

app.MapGet("/tickets", (TicketController controller) => 
{
	return controller.GetAllTickets();
});

//this is a query parameter, it has a parameter to actually be implemented
app.MapGet("/user", (string username, UserController controller) => 
{

	return controller.GetByUsername(username);

});

/*app.MapGet("/users", (string? username, UserController controller) => //this just doesn't work
{
    if(username == null)
    {
        return controller.GetAllUsers();
    }
	else 
	{
		return controller.GetByUsername((string) username); //because we declared it as nullable earlier
	}
});*/

app.MapGet("/ticket/author", (int ID, TicketController controller) =>
{
	return controller.GetByAuthorID(ID);
});

app.MapGet("/ticket", (string status, TicketController controller) =>
{
	return controller.GetByTicketStatus(status);
});

//this is route parameter
app.MapGet("/user/{ID}", (int ID, UserController controller) =>
{
	return controller.GetByUserID(ID);
});

app.MapGet("/ticket/{ID}", (int ID, TicketController controller) =>
{
	return controller.GetByTicketID(ID);
});

app.MapPost("/register", (Users user, AuthController controller) => //couldn't we technically do these through query param? yes technically
{
	return controller.RegisterUser(user);
});

app.MapPost("/login", (string username, string password, AuthController controller) => 
{
	return controller.Login(username, password);
});

app.MapPost("/ticket/submit", (Tickets ticket, TicketController controller) => 
{
	return controller.CreateTicket(ticket);
});

app.MapPost("/ticket/process", (int ticketID, string ticketStatus, int resolverID, TicketController controller) => 
{
	controller.UpdateTicket(ticketID, ticketStatus, resolverID);
});


app.Run();
