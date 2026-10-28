using BankApi.Domain.Entities;
using BankApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Infrastructure.Repositories;

public class CustomerRepository
{
    private readonly AppDbContext _context;
    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Customer?> GetByIdAsync(Guid id) 
        => await _context.Customers
          .Include(c => c.Accounts)
        .FirstOrDefaultAsync(c => c.Id == id);

    public async Task<Customer?> GetByEmailAsync(string email)
        => await _context.Customers
            .FirstOrDefaultAsync(c => c.Email == email);

    public async Task<bool> EmailExistsAsync(string email)
        => await _context.Customers.AnyAsync(c => c.Email == email);

    public async Task AddAsync(Customer customer)
        => await _context.Customers.AddAsync(customer);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();        


}