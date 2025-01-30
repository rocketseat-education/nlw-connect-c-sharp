using System.Net;

namespace TechLibrary.Exception;

public class ConflictException : TechLibraryException
{
    public ConflictException(string message) : base(message) { }
    public override List<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Conflict;
}