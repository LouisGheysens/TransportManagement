namespace BussinesLayer.Exceptions
{
    public class ChassisnummerException : Exception
    {
        public ChassisnummerException(string? message) : base(message)
        {
        }

        public ChassisnummerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
