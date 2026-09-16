namespace BankApi.Domain.Entities;

public class AuditLog
{
    public Guid Id { get; private set; }
    public string Action { get; private set; } = string.Empty;
    public string Entity { get; private set; } = string.Empty;
    public string? EntityId { get; private set; }
    public string? PerformedBy { get; private set; } // customer email
    public string? Details { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    private AuditLog() { }

    public static AuditLog Create(string action, string entity, string? entityId, string? performedBy, string? details = null)
    {
        return new AuditLog
        {
            Id = Guid.NewGuid(),
            Action = action,
            Entity = entity,
            EntityId = entityId,
            PerformedBy = performedBy,
            Details = details,
            CreatedAtUtc = DateTime.UtcNow
        };
    }
}