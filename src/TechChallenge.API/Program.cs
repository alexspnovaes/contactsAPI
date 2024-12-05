using Prometheus;
using Prometheus.SystemMetrics;
using TechChallenge.Api.Extensions;
using TechChallenge.API.Logging;
using TechChallenge.Domain.Entities;
using TechChallenge.Infrastructure.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

builder.Logging.ClearProviders();

builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderProviderConfiguration
{
    LogLevel = LogLevel.Information
}));

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddSystemMetrics();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    SeedDatabase(context);
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseHttpMetrics(); 
app.UseMetricServer(); 

app.UseAuthorization();

app.MapControllers();
app.Run();

void SeedDatabase(ApplicationDbContext context)
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    context.Contacts.AddRange(
        new Contact { Id = Guid.NewGuid(), Name = "João Silva", PhoneNumber = "991778080", Ddd = "016", Email = "test@test.com" },
        new Contact { Id = Guid.NewGuid(), Name = "Pedro Henrique", PhoneNumber = "98776655", Ddd = "016", Email = "test2@test.com" },
        new Contact { Id = Guid.NewGuid(), Name = "Manuel Cardoso", PhoneNumber = "909010201", Ddd = "051", Email = "test3@test.com" }
    );

    context.SaveChanges();
}