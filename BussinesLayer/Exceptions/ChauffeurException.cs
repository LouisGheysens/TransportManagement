namespace BussinesLayer.Exceptions
{
    public class ChauffeurException : Exception
    {
        public ChauffeurException(string? message) : base(message)
        {
        }

        public ChauffeurException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
