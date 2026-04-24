namespace ShatteredRealms.Application.Interfaces;

public interface IFileStorageService
{
    Task SaveAsync(Stream content, string relativePath, CancellationToken ct = default);
    Task DeleteAsync(string relativePath, CancellationToken ct = default);
    Stream Open(string relativePath);
}
