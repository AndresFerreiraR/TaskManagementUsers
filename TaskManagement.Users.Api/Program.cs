using Microsoft.EntityFrameworkCore;
using TaskManagement.Users.Application;
using TaskManagement.Users.Infrastructure.Context;
using TaskManagement.Users.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();

builder.Services.AddApplicationService();
builder.Services.AddInfraestructureServices();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");

builder.Services.AddDbContext<UserTaskContext>(opt => 
{
    opt.UseSqlServer(connectionString);
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();

