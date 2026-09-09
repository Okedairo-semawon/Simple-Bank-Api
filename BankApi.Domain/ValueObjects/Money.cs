namespace BankApi.Domain.ValueObjects;

public record Money 
{
    public decimal Amount {get; init;}
    public string Currency {get; init;}

    public Money(decimal amount, string currency)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative", name of amount);
        
        if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
            throw new ArgumentException("Currency must be a valid 3-letter ISO code", name of currency);

        Amount = Math.Round(amount, 2);
        Currency = currency.ToUpper();
    }

    public Money Add(Money other)
    {
        EnsureSameCurrency(other);
        return new Money (Amount + other.Amount, Currency)
    }

    public Money Subtract(other);
    {
        EnsureSameCurrency(other);
        if (Amount < other.Amount)
            throw new InvalidOperationException("Insufficient funds for this operation");
        return new Money(Amount - other.Amount, Currency);    
    }

    public bool isGreaterThan(Money other);
    {
        EnsureSameCurrency(other);
        return Amount > other.Amount;
    }

        private void EnsureSameCurrency(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException($"Currency mismatch: {Currency} vs {other.Currency}.");
    }

    public override string ToString() => $"{Amount:F2} {Currency}";
}