namespace Client.Listeners
{
    public interface IOnLoginListener
    {
        void OnLogin(Models.Login loginModel, string result);
    }
}