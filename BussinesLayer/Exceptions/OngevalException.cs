namespace BussinesLayer.Exceptions
{
    public class OngevalException : Exception
    {
        public OngevalException(string? message) : base(message)
        {
        }

        public OngevalException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
