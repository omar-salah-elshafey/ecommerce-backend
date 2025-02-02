namespace Application.ExceptionHandling
{
    public class EmailAlreadyConfirmedException : Exception
    {
        public EmailAlreadyConfirmedException(string message) : base(message) { }
    }
}
