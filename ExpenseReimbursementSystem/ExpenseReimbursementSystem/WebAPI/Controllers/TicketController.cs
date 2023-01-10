using Services;
using Models;
using CustomExceptions;

namespace WebAPI.Controllers;

public class TicketController
{
	private readonly TicketService _service;

    public TicketController(TicketService service) {
        _service = service;
    }

    public List<Tickets> GetAllTickets()  {
        return _service.GetAllTickets();
    }

    public IResult CreateTicket(Tickets ticketToSubmit) {
        try
        {
            _service.CreateTicket(ticketToSubmit);
            return Results.Created("/ticket/submit", ticketToSubmit);
        }
        catch (UnsuccessfulTicketSubmission)
        {
            return Results.BadRequest("That is not a valid ticket submission.");
        }
    }

    public IResult GetByAuthorID(int ID) {
        List<Tickets> ticketinfo = _service.GetByAuthorID(ID);
        if (ticketinfo.Count < 1) 
        {
            return Results.BadRequest("That user does not have any tickets."); 
        }
        try 
        {  
            return Results.Ok(ticketinfo); 
        }
        catch(ResourceNotFound)
        {
            return Results.BadRequest("That user does not have any tickets.");
        }
    }

    public IResult GetByTicketStatus(string status) {
        if (status != "Approved" && status != "Pending" && status != "Denied")
        {
            return Results.BadRequest("You entered an invalid status.");
        }
        try 
        {  
            List<Tickets> ticketinfo = _service.GetByTicketStatus(status);
            return Results.Ok(ticketinfo); 
        }
        catch(ResourceNotFound)
        {
            return Results.BadRequest("You entered an invalid status.");
        }
    }

    public void UpdateTicket(int ticketID, string ticketStatus, int resolverID) {
        _service.UpdateTicket(ticketID, ticketStatus, resolverID);
    }

    public IResult GetByTicketID(int ID) {
        Tickets ticketinfo = _service.GetByTicketID(ID);
        if (ticketinfo.ticketID == 0)
        {
            return Results.BadRequest("A ticket with this ID does not exist.");
        }
        try 
        {  
            return Results.Ok(ticketinfo); 
        }
        catch(ResourceNotFound)
        {
            return Results.BadRequest("A ticket with this ID does not exist.");
        }
    }
}