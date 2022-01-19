namespace BussinesLayer.Interfaces
{
    public interface IAuthenticationService
    {
        User AuthenticateUser(string username, string password);
    }
}
