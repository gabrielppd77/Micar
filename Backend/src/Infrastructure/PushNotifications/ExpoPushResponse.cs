namespace Infrastructure.PushNotifications;

public class ExpoPushResponse
{
    public List<ExpoPushTicket> Data { get; set; } = new();
}

public class ExpoPushTicket
{
    public string Status { get; set; } = string.Empty;
    public string? Message { get; set; }
    public ExpoPushTicketDetails? Details { get; set; }
}

public class ExpoPushTicketDetails
{
    public string? Error { get; set; }
}
