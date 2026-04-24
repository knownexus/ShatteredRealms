using MediatR;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Documents.Commands;

public record DeleteDocumentCommand(int Id) : IRequest<Result>;
