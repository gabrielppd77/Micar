namespace Contracts.Authentications;

public interface ICurrentUserService
{
    Guid GetUserId();
}
