using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using ShatteredRealms.Application.Interfaces;

namespace ShatteredRealms.Infrastructure.Services;

public sealed class LocalFileStorageService : IFileStorageService
{
    private readonly string _basePath;

    public LocalFileStorageService(IConfiguration configuration, IWebHostEnvironment env)
    {
        var configured = configuration["Storage:BasePath"] ?? "uploads";
        _basePath = Path.IsPathRooted(configured)
            ? configured
            : Path.Combine(env.ContentRootPath, configured);

        Directory.CreateDirectory(_basePath);
    }

    public async Task SaveAsync(Stream content, string relativePath, CancellationToken ct = default)
    {
        var fullPath = Path.Combine(_basePath, relativePath);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
        await using var file = File.Create(fullPath);
        await content.CopyToAsync(file, ct);
    }

    public Task DeleteAsync(string relativePath, CancellationToken ct = default)
    {
        var fullPath = Path.Combine(_basePath, relativePath);
        if (File.Exists(fullPath))
            File.Delete(fullPath);
        return Task.CompletedTask;
    }

    public Stream Open(string relativePath)
        => File.OpenRead(Path.Combine(_basePath, relativePath));
}
