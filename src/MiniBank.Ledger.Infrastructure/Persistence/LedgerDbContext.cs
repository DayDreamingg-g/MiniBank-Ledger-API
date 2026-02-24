using Microsoft.EntityFrameworkCore;
using MiniBank.Ledger.Application.Common.Interfaces;
using MiniBank.Ledger.Domain.Entities;

namespace MiniBank.Ledger.Infrastructure.Persistence;

public sealed class LedgerDbContext : DbContext, ILedgerDbContext
{
    public LedgerDbContext(DbContextOptions<LedgerDbContext> options) : base(options)
    {
    }

    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Transaction>(b =>
        {
            b.ToTable("transactions");

            b.HasKey(x => x.Id);

            b.Property(x => x.Amount)
                .HasColumnName("amount")
                .HasPrecision(18, 2)
                .IsRequired();

            b.Property(x => x.Currency)
                .HasColumnName("currency")
                .HasMaxLength(3)
                .IsRequired();

            b.Property(x => x.Date)
                .HasColumnName("date")
                .IsRequired();

            b.Property(x => x.Category)
                .HasColumnName("category")
                .HasMaxLength(64)
                .IsRequired();

            b.Property(x => x.Description)
                .HasColumnName("description")
                .HasMaxLength(256);

            b.Property(x => x.Type)
                .HasColumnName("type")
                .HasConversion<int>()
                .IsRequired();
        });
    }
}