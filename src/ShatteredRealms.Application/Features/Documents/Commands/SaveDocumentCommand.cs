using MediatR;
using ShatteredRealms.Application.DTOs.Documents;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Documents.Commands;

public record SaveDocumentCommand(
    string UploadedById,
    string OriginalFileName,
    string StoredFileName,
    string ContentType,
    long FileSizeBytes
) : IRequest<Result<DocumentDto>>;
