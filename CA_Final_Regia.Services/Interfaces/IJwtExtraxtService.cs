namespace CA_Final_Regia.Services.Interfaces
{
    public interface IJwtExtraxtService
    {
        Guid GetAccountIdFromJwtToken(string authorizationHeader);
    }
}
