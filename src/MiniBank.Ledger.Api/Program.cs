using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniBank.Ledger.Application;
using MiniBank.Ledger.Application.Accounts.Queries.GetAccounts;
using MiniBank.Ledger.Application.Common.Interfaces;
using MiniBank.Ledger.Application.Reports.Queries.GetSummaryReport;
using MiniBank.Ledger.Application.Transactions.Commands.CreateTransaction;
using MiniBank.Ledger.Application.Transactions.Queries.GetTransactions;
using MiniBank.Ledger.Infrastructure.Persistence;
using MiniBank.Ledger.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly));

builder.Services.AddScoped<IAccountRepository, InMemoryAccountRepository>();

var cs = builder.Configuration.GetConnectionString("LedgerDb")
         ?? throw new InvalidOperationException("Connection string 'LedgerDb' is missing.");

builder.Services.AddDbContext<LedgerDbContext>(options =>
    options.UseSqlite(cs));

builder.Services.AddScoped<ILedgerDbContext>(sp => sp.GetRequiredService<LedgerDbContext>());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// POST /transactions (CQRS)
app.MapPost("/transactions", async (CreateTransactionCommand command, IMediator mediator) =>
{
    var id = await mediator.Send(command);
    return Results.Created($"/transactions/{id}", new { id });
})
.WithName("CreateTransaction");

// GET /transactions?from=2026-02-01&to=2026-02-28
app.MapGet("/transactions", async (DateTime? from, DateTime? to, IMediator mediator) =>
{
    var result = await mediator.Send(new GetTransactionsQuery(from, to));
    return Results.Ok(result);
})
.WithName("GetTransactions");

// GET /reports/summary?from=2026-02-01&to=2026-02-28
app.MapGet("/reports/summary", async (DateTime? from, DateTime? to, IMediator mediator) =>
{
    var result = await mediator.Send(new GetSummaryReportQuery(from, to));
    return Results.Ok(result);
})
.WithName("GetSummaryReport");

// GET /accounts (CQRS)
app.MapGet("/accounts", async (IMediator mediator) =>
{
    var result = await mediator.Send(new GetAccountsQuery());
    return Results.Ok(result);
})
.WithName("GetAccounts");

app.Run();