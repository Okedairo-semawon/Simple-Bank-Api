namespace BankApi.Models;

public enum TransactionType
{
    Deposit,
    Withdrawal,
    Transfer

}

public class Transaction

{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public TransactionType Type { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Foreign key
    public int AccountId { get; set; }

    // For transfers — which account was the other side
    public int? TargetAccountId { get; set; }

    // Navigation properties
    public Account Account { get; set; } = null!;
}