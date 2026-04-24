using Microsoft.Extensions.FileProviders;
using Serilog;
using ShatteredRealms.API.Extensions;

Log.Logger = ConfigurationExtension.ConfigureLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationDatabase(builder.Configuration, builder.Environment);
builder.Services.AddApplicationIdentity();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();
builder.Services.AddApplicationCors();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await DatabaseSeeder.SeedDatabase(services: scope.ServiceProvider);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorClient");

var uploadsPath = Path.Combine(app.Environment.ContentRootPath,
    builder.Configuration["Storage:BasePath"] ?? "uploads");
Directory.CreateDirectory(uploadsPath);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsPath),
    RequestPath  = "/uploads",
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}

