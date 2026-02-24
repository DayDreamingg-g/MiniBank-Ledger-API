using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniBank.Ledger.Api.Contracts;
using MiniBank.Ledger.Application;
using MiniBank.Ledger.Application.Accounts.Queries.GetAccounts;
using MiniBank.Ledger.Application.Common.Interfaces;
using MiniBank.Ledger.Domain.Entities;
using MiniBank.Ledger.Infrastructure.Persistence;
using MiniBank.Ledger.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// MediatR (register handlers from Application assembly)
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly));

// Repo implementation (quick CQRS integrity check)
builder.Services.AddScoped<IAccountRepository, InMemoryAccountRepository>();

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

// GET /accounts (CQRS check)
app.MapGet("/accounts", async (IMediator mediator) =>
{
    var result = await mediator.Send(new GetAccountsQuery());
    return Results.Ok(result);
})
.WithName("GetAccounts");

app.Run();