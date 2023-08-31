namespace API.Errors;

public class ApiResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    
    public ApiResponse(int statusCode, string message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);

    }

    private string? GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "You've made a bad request",
            401 => "Not Authorized",
            404 => "No resource found",
            500 => "Server error...",
            _ => null
        };
    }
}