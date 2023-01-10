using System.Data.SqlClient;
using DAO;
using Models;
using System.Collections.Generic;
using CustomExceptions;

namespace Services;

public class TicketService
{
	TicketsDAO ticketaccess = new TicketsDAO();

	public List<Tickets> GetAllTickets() {
		return ticketaccess.GetAllTickets();
	}

	public Tickets CreateTicket(Tickets ticket) {
		try 
		{
			return ticketaccess.CreateTicket(ticket);
	    }
	    catch(UnsuccessfulTicketSubmission)
        {
            throw new UnsuccessfulTicketSubmission();
        }
	}

	public void UpdateTicket(int ticketID, string ticketStatus, int resolverID) {
		ticketaccess.UpdateTicket(ticketID, ticketStatus, resolverID);
	}

	public Tickets GetByTicketID(int ID) {
		try
		{
			return ticketaccess.GetByTicketID(ID);
		}
		catch(ResourceNotFound)
        {
            throw new ResourceNotFound();
        }
	}

	public List<Tickets> GetByAuthorID(int ID) {
        try
        {
        	return ticketaccess.GetByAuthorID(ID);
        }
        catch(ResourceNotFound)
        {
            throw new ResourceNotFound();
        }
	}

	public List<Tickets> GetByTicketStatus(string status) {
		try
		{
			return ticketaccess.GetByTicketStatus(status);
		}
		catch(ResourceNotFound)
        {
            throw new ResourceNotFound();
        }
	}	
}