namespace CA_Final_Regia.Services.Interfaces
{
    public interface IJwtExtractService
    {
        Guid GetAccountIdFromJwtToken(string authorizationHeader);
    }
}
