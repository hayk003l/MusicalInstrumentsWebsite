
namespace Service.Contracts
{
    public interface IUserContextService
    {
        Guid GetUserId();
        string GetUserRole();
        string GetUserUsername();

        bool IsAuthenticated();
        bool IsInRole(string role);
    }
}
