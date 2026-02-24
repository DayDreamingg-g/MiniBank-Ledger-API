using Microsoft.EntityFrameworkCore;
using MiniBank.Ledger.Api.Contracts;
using MiniBank.Ledger.Domain.Entities;
using MiniBank.Ledger.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var cs = builder.Configuration.GetConnectionString("LedgerDb")
         ?? throw new InvalidOperationException("Connection string 'LedgerDb' is missing.");

builder.Services.AddDbContext<LedgerDbContext>(options =>
    options.UseSqlite(cs));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// POST /transactions
app.MapPost("/transactions", async (CreateTransactionRequest req, LedgerDbContext db) =>
{
    // Domain validation happens inside the entity constructor (invariants)
    var tx = new Transaction(
        amount: req.Amount,
        currency: req.Currency,
        date: req.Date,
        category: req.Category,
        description: req.Description ?? string.Empty,
        type: req.Type);

    db.Transactions.Add(tx);
    await db.SaveChangesAsync();

    return Results.Created($"/transactions/{tx.Id}", new
    {
        tx.Id,
        tx.Amount,
        tx.Currency,
        tx.Date,
        tx.Category,
        tx.Description,
        tx.Type
    });
})
.WithName("CreateTransaction");

app.Run();