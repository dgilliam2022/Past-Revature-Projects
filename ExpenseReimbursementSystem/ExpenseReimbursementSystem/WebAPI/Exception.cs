namespace CustomExceptions;


public class ResourceNotFound : Exception
{
    public ResourceNotFound() { }
    public ResourceNotFound(string message) : base(message) { }
    public ResourceNotFound(string message, System.Exception inner) : base(message, inner) { }
    protected ResourceNotFound(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

public class UsernameNotAvailable : Exception
{
    public UsernameNotAvailable() { }
    public UsernameNotAvailable(string message) : base(message) { }
    public UsernameNotAvailable(string message, System.Exception inner) : base(message, inner) { }
    protected UsernameNotAvailable(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
public class InvalidCredentials : Exception
{
    public InvalidCredentials() { }
    public InvalidCredentials(string message) : base(message) { }
    public InvalidCredentials(string message, System.Exception inner) : base(message, inner) { }
    protected InvalidCredentials(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

public class UnsuccessfulRegistration : Exception
{
    public UnsuccessfulRegistration() { }
    public UnsuccessfulRegistration(string message) : base(message) { }
    public UnsuccessfulRegistration(string message, System.Exception inner) : base(message, inner) { }
    protected UnsuccessfulRegistration(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

public class UnsuccessfulTicketSubmission : Exception
{
    public UnsuccessfulTicketSubmission() { }
    public UnsuccessfulTicketSubmission(string message) : base(message) { }
    public UnsuccessfulTicketSubmission(string message, System.Exception inner) : base(message, inner) { }
    protected UnsuccessfulTicketSubmission(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}