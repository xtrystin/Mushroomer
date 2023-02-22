using Application.Queries;
using Domain.Repository;
using Infrastructure.Dapper.Repository;
using Infrastructure.EF;
using Infrastructure.EF.Queries;
using Infrastructure.EF.Repository;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using WebAPI.Controllers;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("OpenCorsPolicy", opt =>
        opt.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

//builder.Services.AddMSSqlServer(builder.Configuration);
builder.Services.AddPostgres(builder.Configuration);
builder.Services.AddScoped<IWarningRepository, WarningRepository>();    //todo: move it to extension method?
builder.Services.AddScoped<IUserRepository, UserRepository>();    //todo: move it to extension method?

builder.Services.AddMediatR(typeof(GetAllWarningsQueryHandler), typeof(GetAllWarningsQuery));
//builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());   //todo: test it
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithJwtAuth();

builder.Services.AddJwtAuthentication(builder.Configuration);

// Register httpClients
builder.Services.AddHttpClient<PostController>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["MicroservicesUrl:Post"]);
});

//// Add Ocelot
//builder.Configuration.AddJsonFile("ocelot.json", false, true);
//builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("OpenCorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
