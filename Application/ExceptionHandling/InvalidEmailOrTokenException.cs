namespace Application.ExceptionHandling
{
    public class InvalidEmailOrTokenException : Exception
    {
        public InvalidEmailOrTokenException(string message) : base(message) { }
    }
}
