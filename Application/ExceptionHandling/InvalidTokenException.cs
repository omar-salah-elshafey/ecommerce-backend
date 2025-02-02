namespace Application.ExceptionHandling
{
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException(string message) : base(message) { }
    }
}
