using MediatR;
using MiniBank.Ledger.Application.Common.Dtos;

namespace MiniBank.Ledger.Application.Reports.Queries.GetSummaryReport;

public sealed record GetSummaryReportQuery(DateTime? From, DateTime? To)
    : IRequest<SummaryReportDto>;