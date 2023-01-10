namespace Models;

public class Tickets
{
   public int ticketID { get; set; }
   public int authorID { get; set; }
   public int resolverID { get; set; } 
   public string ticketDescription { get; set; }
   public string ticketStatus { get; set; }
   public double ticketAmount { get; set; }

   public Tickets(){

   }

   public Tickets(int authorID, string ticketDescription, double ticketAmount) {
      this.authorID = authorID;
      this.ticketDescription = ticketDescription;
      this.ticketAmount = ticketAmount;
   }

   public Tickets(int ticketID, int authorID, int resolverID, string ticketDescription, string ticketStatus, double ticketAmount) {
      this.ticketID = ticketID;
      this.authorID = authorID;
      this.resolverID = resolverID;
      this.ticketDescription = ticketDescription;
      this.ticketStatus = ticketStatus;
      this.ticketAmount = ticketAmount;
   }

   public override string ToString() {
        return "Ticket ID: " + this.ticketID +
        ", Author ID: " + this.authorID +
        ", Resolver ID: " + this.resolverID +
        ", Ticket description: " + this.ticketDescription +
        ", Ticket status: " + this.ticketStatus +
        ", Ticket amount: " + this.ticketAmount;
   }
   
 
}

