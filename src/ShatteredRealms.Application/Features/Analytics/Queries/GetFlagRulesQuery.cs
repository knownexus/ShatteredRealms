using MediatR;
using ShatteredRealms.Application.DTOs.Analytics;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Analytics.Queries;

public record GetFlagRulesQuery : IRequest<Result<List<AnalyticsFlagRuleDto>>>;
