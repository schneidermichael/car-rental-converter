using grpc_currency_converter.Services;
using Microsoft.AspNetCore.Authentication.Certificate;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
    .AddCertificate(options =>
    {
        //Not recommended in production environments. The example is using a self-signed test certificate.
        options.RevocationMode = X509RevocationMode.NoCheck;
        options.AllowedCertificateTypes = CertificateTypes.All;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRouting();

app.UseAuthentication();

app.MapGrpcService<CurrencyConverterImpl>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. ");

app.Run();
