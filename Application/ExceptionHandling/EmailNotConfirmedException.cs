namespace Application.ExceptionHandling
{
    public class EmailNotConfirmedException : Exception
    {
        public EmailNotConfirmedException(string message) : base(message) { }
    }
}
