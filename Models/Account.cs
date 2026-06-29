namespace BankApi.Models;

public enum AccountType
{
    Checking,
    Savings,
    Credit
}

public class Account 
{
    public int Id {get; set;} 
    public string AccountNumber {get; set;}
    public AccountType Type {get; set;} 
    public decimal Balance {get; set;} = 0;
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    
    // foreign key to User
    public int UserId {get; set;}

     // Navigation properties
    public User User { get; set; } = null!;
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}