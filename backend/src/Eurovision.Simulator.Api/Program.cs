using Eurovision.Simulator.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterInfrastructureServices();

var app = builder.Build();
app.UseHttpsRedirection();
app.Run();
