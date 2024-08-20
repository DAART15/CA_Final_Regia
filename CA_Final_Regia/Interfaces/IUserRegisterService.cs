namespace CA_Final_Regia.Interfaces
{
    public interface IUserRegisterService
    {
        Task Register(string username, string password);
    }
}
