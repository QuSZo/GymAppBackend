using GymAppBackend.Application;
using GymAppBackend.Core;
using GymAppBackend.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure();

var app = builder.Build();
app.UseInfrastructure();
app.Run();