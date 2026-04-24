using MediatR;
using ShatteredRealms.Application.DTOs.Documents;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Documents.Queries;

public record GetAllDocumentsQuery : IRequest<Result<List<DocumentDto>>>;
