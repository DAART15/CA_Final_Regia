namespace CA_Final_Regia.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string username, string role);
    }
}
