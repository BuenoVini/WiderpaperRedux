namespace Widerpaper;

public class WiderpaperException : Exception
{
    public WiderpaperException() { }
    public WiderpaperException(string message) : base(message) { }
    public WiderpaperException(string message,  Exception innerException) : base(message, innerException) { }
}
