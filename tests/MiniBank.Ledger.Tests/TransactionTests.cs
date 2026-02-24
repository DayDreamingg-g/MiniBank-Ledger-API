using MiniBank.Ledger.Domain.Entities;
using MiniBank.Ledger.Domain.Enums;

namespace MiniBank.Ledger.Tests;

public class TransactionTests
{
    [Fact]
    public void Constructor_Should_Create_Transaction_With_Valid_Data()
    {
        var tx = new Transaction(
            amount: 10m,
            currency: "usd",
            date: new DateTime(2026, 2, 24),
            category: "Food",
            description: "Lunch",
            type: TransactionType.Expense);

        Assert.NotEqual(Guid.Empty, tx.Id);
        Assert.Equal(10m, tx.Amount);
        Assert.Equal("USD", tx.Currency);
        Assert.Equal("Food", tx.Category);
        Assert.Equal("Lunch", tx.Description);
        Assert.Equal(TransactionType.Expense, tx.Type);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_Should_Throw_When_Amount_Is_Not_Positive(decimal amount)
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new Transaction(amount, "USD", DateTime.UtcNow, "Food", "Test", TransactionType.Expense));

        Assert.Contains("Amount", ex.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_Should_Throw_When_Currency_Is_Empty(string currency)
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new Transaction(1m, currency, DateTime.UtcNow, "Food", "Test", TransactionType.Expense));

        Assert.Contains("Currency", ex.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_Should_Throw_When_Category_Is_Empty(string category)
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new Transaction(1m, "USD", DateTime.UtcNow, category, "Test", TransactionType.Expense));

        Assert.Contains("Category", ex.Message);
    }
}