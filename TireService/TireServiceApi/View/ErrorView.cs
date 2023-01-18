namespace TireServiceApi.View;

public class ErrorView
{
    public string Type { get; set; }
    public string Message { get; set; }
    public string[] StackTrace { get; set; }
}
