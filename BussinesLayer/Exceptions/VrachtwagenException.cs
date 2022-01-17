namespace BussinesLayer.Exceptions
{
    public class VrachtwagenException : Exception
    {
        public VrachtwagenException(string? message) : base(message)
        {
        }

        public VrachtwagenException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
