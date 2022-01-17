namespace DataLayer.Exceptions
{
    public class VrachtwagenRepositoryException : Exception
    {
        public VrachtwagenRepositoryException(string? message) : base(message)
        {
        }

        public VrachtwagenRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
