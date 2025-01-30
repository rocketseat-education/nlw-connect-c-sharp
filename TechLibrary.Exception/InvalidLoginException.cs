using System.Net;

namespace TechLibrary.Exception;

public class InvalidLoginException : TechLibraryException
{

    public InvalidLoginException() : base("Email e/ou senha inválidos.") { }
    public override List<string> GetErrorMessages() => [Message];
    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}