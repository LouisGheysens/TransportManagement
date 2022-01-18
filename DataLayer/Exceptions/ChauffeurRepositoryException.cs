namespace DataLayer.Exceptions
{
    public class ChauffeurRepositoryException : Exception
    {
        public ChauffeurRepositoryException(string? message) : base(message)
        {
        }

        public ChauffeurRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
