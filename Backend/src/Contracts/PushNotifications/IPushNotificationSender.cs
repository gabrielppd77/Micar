using Domain.PushNotifications;

namespace Contracts.PushNotifications;

public interface IPushNotificationSender
{
    Task<IReadOnlyList<PushNotificationResultado>> EnviarAsync(
        IEnumerable<string> tokens,
        string titulo,
        string corpo,
        CancellationToken ct);
}
