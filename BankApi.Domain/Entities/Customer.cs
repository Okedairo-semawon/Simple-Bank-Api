namespace BankApi.Domain.Entities;

public class Customer 
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string FullName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public bool IsVerified { get; private set; } = false;
    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;

    // Navigation
    public ICollection<Account> Accounts { get; private set; } = new List<Account>();

    // EF Core needs this
    private Customer() { }

      public static Customer Create(string fullName, string email, string passwordHash, string phoneNumber)
    {
        return new Customer
        {
            Id = Guid.NewGuid(),
            FullName = fullName,
            Email = email,
            PasswordHash = passwordHash,
            PhoneNumber = phoneNumber
        };
    }

    public void Verify() => IsVerified = true;

    public void UpdateProfile(string fullName, string phoneNumber)
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;
    }
}