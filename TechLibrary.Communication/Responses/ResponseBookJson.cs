namespace TechLibrary.Communication.Responses;

public class ResponseBookJson
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
}