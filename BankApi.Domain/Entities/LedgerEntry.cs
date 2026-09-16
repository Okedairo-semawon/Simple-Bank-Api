using BankApi.Domain.Enums;

namespace BankApi.Domain.Entities;

public class LedgerEntry
{
    public Guid Id { get; private set; }
    public Guid TransactionId { get; private set; } // groups debit + credit pair
    public Guid AccountId { get; private set; }
    public decimal Amount { get; private set; }
    public EntryType Type { get; private set; } // Debit or Credit
    public string? Description { get; private set; }
    public string? IdempotencyKey { get; private set; } // prevents duplicate transactions
    public DateTime CreatedAtUtc { get; private set; }

    // Navigation
    public Account Account { get; private set; } = null!;

    private LedgerEntry() { }

    public static LedgerEntry Create(
        Guid transactionId,
        Guid accountId,
        decimal amount,
        EntryType type,
        string? description = null,
        string? idempotencyKey = null)
    {
        return new LedgerEntry
        {
            Id = Guid.NewGuid(),
            TransactionId = transactionId,
            AccountId = accountId,
            Amount = amount,
            Type = type,
            Description = description,
            IdempotencyKey = idempotencyKey,
            CreatedAtUtc = DateTime.UtcNow
        };
    }
}