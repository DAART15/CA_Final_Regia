namespace CA_Final_Regia.Services.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(string username, string role);
    }
}
