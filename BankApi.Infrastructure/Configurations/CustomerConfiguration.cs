using BankApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankApi.Infrastructure.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);
            
            builder.Property(c => c.FullName)
                .IsRequired()
                .HasMaxLength(100);
    
            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(150);

            buidlder.HasIndex(c => c.Email)
                .IsUnique()
        
            builder.Property(c => c.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);
    
            builder.Property(c => c.PasswordHash)
                .IsRequired();

            builder.HasMany(c => c.Accounts)
                .WithOne(a => a.Customer)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
    }
}