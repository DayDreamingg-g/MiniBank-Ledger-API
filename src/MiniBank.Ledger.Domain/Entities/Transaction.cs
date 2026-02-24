using MiniBank.Ledger.Domain.Enums;

namespace MiniBank.Ledger.Domain.Entities;

public class Transaction
{
    public Guid Id { get; private set; }

    public decimal Amount { get; private set; }

    public string Currency { get; private set; } = string.Empty;

    public DateTime Date { get; private set; }

    public string Category { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public TransactionType Type { get; private set; }

    // Для EF Core
    private Transaction() { }

    public Transaction(
        decimal amount,
        string currency,
        DateTime date,
        string category,
        string description,
        TransactionType type)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.", nameof(amount));

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency is required.", nameof(currency));

        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Category is required.", nameof(category));

        Id = Guid.NewGuid();
        Amount = amount;
        Currency = currency.Trim().ToUpperInvariant();
        Date = date;
        Category = category.Trim();
        Description = description?.Trim() ?? string.Empty;
        Type = type;
    }
}