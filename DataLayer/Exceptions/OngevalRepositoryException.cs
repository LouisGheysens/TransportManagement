namespace DataLayer.Exceptions
{
    public class OngevalRepositoryException : Exception
    {
        public OngevalRepositoryException(string? message) : base(message)
        {
        }

        public OngevalRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
