using BankApi.Domain.Entities;
using BankApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Infrastructure.Repositories;

public class LedgerRepository
{
    private readonly AppDbContext _context;

    public LedgerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IdempotencyKeyExistsAsync(string key)
        => await _context.LedgerEntries
            .AnyAsync(l => l.IdempotencyKey == key);

    public async Task<IEnumerable<LedgerEntry>> GetByAccountIdAsync(Guid accountId)
        => await _context.LedgerEntries
            .Where(l => l.AccountId == accountId)
            .OrderByDescending(l => l.CreatedAtUtc)
            .ToListAsync();

    public async Task AddRangeAsync(IEnumerable<LedgerEntry> entries)
        => await _context.LedgerEntries.AddRangeAsync(entries);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}