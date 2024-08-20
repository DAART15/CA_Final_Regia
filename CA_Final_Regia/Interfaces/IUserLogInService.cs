namespace CA_Final_Regia.Interfaces
{
    public interface IUserLogInService
    {
        Task LogIn(string username, string password);
    }
}
