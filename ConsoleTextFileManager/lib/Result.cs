namespace ConsoleTextFileManager.lib;

public class Result<T>
{
    private string Message { get;}
    private bool IsSuccess { get;}
    private T Value { get;}
    
    private Result(string message, bool success, T value)
    {
        Message = message;
        IsSuccess = success;
        Value = value;
    }

    public static Result<T> Success(T value, string message)
    {
        return new Result<T>(message, true, value);
    }
    
    public static Result<T?> Fail(T value, string message)
    {
        return new Result<T?>(message, false, default);
    }
    
}