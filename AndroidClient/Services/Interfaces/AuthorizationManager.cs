namespace Client.Services.Interfaces
{
    public interface IAuthorizationManager
    {
        void ClearAuthorization();

        void SetAuthorization(object data);

        bool CheckAuthorization();
    }
}