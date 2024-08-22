namespace CA_Final_Regia.Interfaces
{
    public interface IJwtExtraxtService
    {
        Guid GetAccountIdFromJwtToken(string authorizationHeader);
    }
}
