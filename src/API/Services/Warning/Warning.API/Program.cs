using Application.Queries;
using Common.Middleware;
using Domain.Repository;
using Infrastructure.EF;
using Infrastructure.EF.Queries;
using Infrastructure.EF.Repository;
using MediatR;
using Warning.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddMSSqlServer(builder.Configuration);
builder.Services.AddPostgres(builder.Configuration);
builder.Services.AddMediatR(typeof(GetAllWarningsQueryHandler), typeof(GetAllWarningsQuery));
//builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());   //todo: test it

// Register repo
builder.Services.AddScoped<IWarningRepository, WarningRepository>();    //todo: move it to extension method?
builder.Services.AddScoped<IUserRepository, UserRepository>();    //todo: move it to extension method?

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithJwtAuth();

builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandlerMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
