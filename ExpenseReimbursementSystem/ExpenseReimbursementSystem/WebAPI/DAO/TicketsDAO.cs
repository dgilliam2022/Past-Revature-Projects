using Sensitive;
using System.Data.SqlClient;
using Models;
using System.Collections.Generic;
using CustomExceptions;

namespace DAO;

//The purpose of this layer is to execute the SQL statements to the database

public class TicketsDAO 
{
	//the connection string from azure (class level variable)
    string connectionString = SensitiveVariables.dbConnString;	
	public List<Tickets> GetAllTickets() {
		List<Tickets> tickets = new List<Tickets>();

        //this defines the sql statement we'd like to execute
        string sql = "select * from ERS.tickets;"; 

		//data type for an active connection
		SqlConnection connection = new SqlConnection(connectionString);
		//data type to reference the sql command you want to do to a specific connection
		SqlCommand command = new SqlCommand(sql, connection);

		try 
		{
		   //opening the connection to the database
		   connection.Open();
		   //storing the result set of a DQL statement into a variable
		   SqlDataReader reader = command.ExecuteReader(); 
		   while (reader.Read()) 
		   {
		   	  tickets.Add(new Tickets((int)reader[0], (int)reader[1], (int)reader[2], (string)reader[3], (string)reader[4], (double)reader[5]));
		   }
		   reader.Close();
		   connection.Close();
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
		}

           return tickets;
	}
	public Tickets CreateTicket(Tickets ticket) {
		//this defines the sql statement we'd like to execute
        string sql = "insert into ERS.tickets (author_ID, ticketDescription, ticketAmount) values (@author_ID, @ticketDescription, @ticketAmount);";

		//data type for an active connection
		SqlConnection connection = new SqlConnection(connectionString);
		//data type to reference the sql command you want to do to a specific connection
		SqlCommand command = new SqlCommand(sql, connection);
		command.Parameters.AddWithValue("@author_ID", ticket.authorID); //AddWithValue assigns variable values
        command.Parameters.AddWithValue("@ticketDescription", ticket.ticketDescription); //could I just reference an older value from the database here?
        command.Parameters.AddWithValue("@ticketAmount", ticket.ticketAmount); //AddWithValue assigns variable values
		try 
		{
		   //opening the connection to the database
		   connection.Open();
		   //this is for DML statements
		   int rowsAffected = command.ExecuteNonQuery();
		   connection.Close();
		   if (rowsAffected != 0)
		   {
		    return ticket;
		   }
		   else { throw new UnsuccessfulTicketSubmission(); }
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
			throw new UnsuccessfulTicketSubmission();
		}	
	}
	public void UpdateTicket(int ticketID, string ticketStatus, int resolverID) {
		//this defines the sql statement we'd like to execute
        string sql = "update ERS.tickets set resolver_ID = @resolver_ID, ticketStatus = @ticketStatus where ticketID = @ticketID;";
        
		//data type for an active connection
		SqlConnection connection = new SqlConnection(connectionString);
		//data type to reference the sql command you want to do to a specific connection
		SqlCommand command = new SqlCommand(sql, connection);
		command.Parameters.AddWithValue("@ticketID", ticketID);
		command.Parameters.AddWithValue("@ticketStatus", ticketStatus);
		command.Parameters.AddWithValue("@resolver_ID", resolverID); //AddWithValue assigns variable values
       
		try 
		{
		   //opening the connection to the database
		   connection.Open();
		   //this is for DML statements
		   int rowsAffected = command.ExecuteNonQuery();
		   connection.Close();
		   if (rowsAffected != 0)
		   {
		   	Console.WriteLine("Ticket ID#" + ticketID + " has been updated by user #" + resolverID);
		   }	   
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
		}
		
	}
	public Tickets GetByTicketID(int ID) {
    	string sql = "select * from ERS.tickets where ticketID = @ID;";

    	SqlConnection connection = new SqlConnection(connectionString);
		//data type to reference the sql command you want to do to a specific connection
		SqlCommand command = new SqlCommand(sql, connection);
		command.Parameters.AddWithValue("@ID", ID); 
             
        Tickets ticketinfo = new Tickets(); 
		try 
		{
		   //opening the connection to the database
		   connection.Open();
		   //storing the result set of a DQL statement into a variable
		   SqlDataReader reader = command.ExecuteReader(); //I'll likely need to modify this for drop table
		   while (reader.Read()) 
		   {
		   	  ticketinfo = new Tickets((int)reader[0], (int)reader[1], (int)reader[2], (string)reader[3], (string)reader[4], (double)reader[5]);
		   }

		   reader.Close();
		   connection.Close();
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
		}

         return ticketinfo; 
    }
    public List<Tickets> GetByAuthorID(int ID) {
    	string sql = "select * from ERS.tickets where author_ID = @ID;";

    	SqlConnection connection = new SqlConnection(connectionString);
		//data type to reference the sql command you want to do to a specific connection
		SqlCommand command = new SqlCommand(sql, connection);
		command.Parameters.AddWithValue("@ID", ID); 
             
        List<Tickets> ticketinfo = new List<Tickets>();
		try 
		{
		   //opening the connection to the database
		   connection.Open();
		   //storing the result set of a DQL statement into a variable
		   SqlDataReader reader = command.ExecuteReader(); //I'll likely need to modify this for drop table
		   while (reader.Read()) 
		   {
		   	  ticketinfo.Add(new Tickets((int)reader[0], (int)reader[1], (int)reader[2], (string)reader[3], (string)reader[4], (double)reader[5]));
		   }

		   reader.Close();
		   connection.Close();
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
		}

         return ticketinfo; 
    }
    public List<Tickets> GetByTicketStatus(string status) {
    	string sql = "select * from ERS.tickets where ticketStatus = @ticketStatus;";

    	SqlConnection connection = new SqlConnection(connectionString);
		//data type to reference the sql command you want to do to a specific connection
		SqlCommand command = new SqlCommand(sql, connection);
		command.Parameters.AddWithValue("@ticketStatus", status); 
             
        List<Tickets> ticketinfo = new List<Tickets>();
		try 
		{
		   //opening the connection to the database
		   connection.Open();
		   //storing the result set of a DQL statement into a variable
		   SqlDataReader reader = command.ExecuteReader(); //I'll likely need to modify this for drop table
		   while (reader.Read()) 
		   {
		   	  ticketinfo.Add(new Tickets((int)reader[0], (int)reader[1], (int)reader[2], (string)reader[3], (string)reader[4], (double)reader[5]));
		   }

		   reader.Close();
		   connection.Close();
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
		}

         return ticketinfo; 
    }
}