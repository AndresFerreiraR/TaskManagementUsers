using TaskManagement.Users.Api.Modules.Injections;
using TaskManagement.Users.Api.Modules.Middleware;
using TaskManagement.Users.Application;
using TaskManagement.Users.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();

builder.Services.AddApplicationService();
builder.Services.AddInfraestructureServices(builder.Configuration);
builder.Services.AddInjection();

string myPolicy = "PolicyApiEcommerce";

builder.Services.AddCors(options => options.AddPolicy(myPolicy, builder => builder.AllowAnyOrigin()
                                                                               .AllowAnyHeader()
                                                                               .AllowAnyMethod()));


builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(myPolicy);
app.UseHttpsRedirection();

app.MapControllers();
app.AddMiddleware();
app.Run();