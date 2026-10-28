using BankApi.Domain.Entities;
using BankApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Infrastructure.Repositories;

public class AccountRepository
{
    private readonly AppDbContext _context;
    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Account?> GetByIdAsync(Guid id)
        => await _context.Accounts
            .Include(a => a.LedgerEntries)
            .FirstOrDefaultAsync(a => a.Id == id);

    public async Task<Account?> GetByAccountNumberAsync(string accountNumber)
        => await _context.Accounts
            .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);

    public async Task<IEnumerable<Account>> GetByCustomerIdAsync(Guid customerId)
        => await _context.Accounts
            .Where(a => a.CustomerId == customerId)
            .ToListAsync();

    public async Task AddAsync(Account account)
        => await _context.Accounts.AddAsync(account);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();

}