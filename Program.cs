using csharp_crud_api.DataContext;
using Microsoft.EntityFrameworkCore;
using Serilog;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using OpenTelemetry.Exporter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<dbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
//builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
// Add support for metrics
builder.Services.AddOpenTelemetry().WithMetrics(builder => builder
    .AddOtlpExporter(opt =>
     {
        opt.Endpoint = new Uri("http://localhost:9090/");
        opt.Protocol = OtlpExportProtocol.HttpProtobuf;
     })
    .SetResourceBuilder(ResourceBuilder
    .CreateDefault().AddService("MetricService"))
    .AddAspNetCoreInstrumentation()
    //.AddConsoleExporter()
   
);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapHealthChecks("/health");
app.UseAuthorization();
app.MapControllers();

 

app.Run();
