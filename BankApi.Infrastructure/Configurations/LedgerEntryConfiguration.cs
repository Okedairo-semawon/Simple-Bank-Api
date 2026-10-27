using BankApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankApi.Infrastructure.Configurations;

public class LedgerEntryConfiguration : IEntityTypeConfiguration<LedgerEntry>
{
    public void Configure(EntityTypeBuilder<LedgerEntry> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Amount)
            .HasPrecision(18, 2);
            .IsRequired();

        builder.Property(l => l.Type)
            .HasConversion<string>();
            .IsRequired();

        builder.Property(l => l.Description)
            .HasMaxLength(500);

        builder.Property(l => l.IdempotencyKey)
            .HasMaxLength(100);

        // Prevent duplicate transactions with same idempotency key
        builder.HasIndex(l => l.IdempotencyKey)
            .IsUnique()
            .HasFilter("\"IdempotencyKey\" IS NOT NULL");

        // Index for fast lookups by transaction group
        builder.HasIndex(l => l.TransactionId);
    }
}