using CrmApp.Server.Extensions;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

//Register Services
ServiceCollectionExtension.RegisterServices(builder.Services, builder.Configuration);

#region ResponseCompression

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;

    options.Providers.Add<GzipCompressionProvider>();

    options.Providers.Add<BrotliCompressionProvider>();

    options.MimeTypes =
            [
        // Default
        "text/plain",
                    "text/css",
                    "application/javascript",
                    "text/html",
                    "application/xml",
                    "text/xml",
                    "application/json",
                    "text/json",
                    // Custom
                    "image/svg+xml",
                     "application/atom+xml"
    ];

    builder.Services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Optimal;
});

#endregion

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add exception handling middleware
app.UseMiddleware<CrmApp.Server.Middleware.ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
