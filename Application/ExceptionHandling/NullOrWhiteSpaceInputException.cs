namespace Application.ExceptionHandling
{
    public class NullOrWhiteSpaceInputException : Exception
    {
        public NullOrWhiteSpaceInputException(string message) : base(message) { }
    }
}
