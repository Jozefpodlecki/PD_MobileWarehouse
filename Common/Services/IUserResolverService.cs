namespace Common.Services
{
    public interface IUserResolverService
    {
        int? GetUserId();
        bool CanUserSeeDetails();
    }
}
