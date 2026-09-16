// using BankApi.Domain.Enums;
// using BankApi.Domain.ValueObjects;

// namespace BankApi.Domain.Entities;

// public class Account 
// {
//     public Guid Id { get; private set; }
//     public string AccountNumber { get; private set; } = string.Empty;
//     public AccountType Type { get; private set; }
//     public AccountStatus Status { get; private set; }
//     public string Currency { get; private set; } = string.Empty;
//     public decimal Balance { get; private set; } = 0; // snapshot — always derived from ledger
//     public DateTime CreatedAtUtc { get; private set; }

//     // Prevents doble spend races 
//     public uint RowVersion {get; private set;}

//     // Foreign key
//     public Guid CustomerId {get; private set;} 

//     // Navigation
//     public Customer Customer { get; private set; } = null!;
//     public ICollection<LedgerEntry> LedgerEntries { get; private set; } = new List<LedgerEntry>();

//     private Account() { }

//     public static Account Create (Guid customerId, AccountType type, string currency)
//     {
//         return new Account {
//             Id = Guid.NewGuid(),
//             AccountNumber= GenerateAccountNumber(),
//             Type = type,
//             Status = AccountStatus.Active,
//             Currency = currency.ToUpper(),
//             Balance = 0,
//             CustomerId = customerId,
//             CreatedAtUtc = DateTime.UtcNow
//         }
//     }

//     public void Debit (decimal amount)
//     {
//         if (Status!= AccountStatus.Active)
//             throw  new InvalidOperationException("Account must be active");

//         if (Balance < Amount )
//             throw new InvalidOperationException("Insuffucient funds");
        
//         Balance -= amount;
//     }

//     public void Credit (decimal amount )
//     {
//         if (Status != AccountStatus.Active)
//             throw InvalidOperationException("Account must be active .");

//         Balance += amount
//     }

//     public void Freeze() => Status = AccountStatus.Freeze;
//     public void Close() => Status = AccountStatus.Closed;
//     public void Activate() => Status = AccountStatus.Active;

//     private static string GenerateAccountNumber()
//     {
//         var random = new Random();
//         return string.Concat(Enumerable.Range(0, 10).Select(_ => random.Next(0, 10).ToString()));
//     }
// }

using BankApi.Domain.Enums;
using BankApi.Domain.ValueObjects;

namespace BankApi.Domain.Entities;

public class Account
{
    public Guid Id { get; private set; }
    public string AccountNumber { get; private set; } = string.Empty;
    public AccountType Type { get; private set; }
    public AccountStatus Status { get; private set; }
    public string Currency { get; private set; } = string.Empty;
    public decimal Balance { get; private set; } = 0; // snapshot — always derived from ledger
    public DateTime CreatedAtUtc { get; private set; }

    // Optimistic concurrency — prevents double-spend races
    public uint RowVersion { get; private set; }

    // Foreign key
    public Guid CustomerId { get; private set; }

    // Navigation
    public Customer Customer { get; private set; } = null!;
    public ICollection<LedgerEntry> LedgerEntries { get; private set; } = new List<LedgerEntry>();

    private Account() { }

    public static Account Create(Guid customerId, AccountType type, string currency)
    {
        return new Account
        {
            Id = Guid.NewGuid(),
            AccountNumber = GenerateAccountNumber(),
            Type = type,
            Status = AccountStatus.Active,
            Currency = currency.ToUpper(),
            Balance = 0,
            CustomerId = customerId,
            CreatedAtUtc = DateTime.UtcNow
        };
    }

    public void Credit(decimal amount)
    {
        if (Status != AccountStatus.Active)
            throw new InvalidOperationException("Account is not active.");
        Balance += amount;
    }

    public void Debit(decimal amount)
    {
        if (Status != AccountStatus.Active)
            throw new InvalidOperationException("Account is not active.");
        if (Balance < amount)
            throw new InvalidOperationException("Insufficient funds.");
        Balance -= amount;
    }

    public void Freeze() => Status = AccountStatus.Frozen;
    public void Close() => Status = AccountStatus.Closed;
    public void Activate() => Status = AccountStatus.Active;

    private static string GenerateAccountNumber()
    {
        var random = new Random();
        return string.Concat(Enumerable.Range(0, 10).Select(_ => random.Next(0, 10).ToString()));
    }
}