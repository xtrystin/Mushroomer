using MediatR;
using User.Application.Query;
using User.Domain.Repository;
using User.Infrastructure.Ef;
using User.Infrastructure.Ef.QueryHandler;
using User.Infrastructure.Ef.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddMSSqlServer(builder.Configuration);
builder.Services.AddPostgres(builder.Configuration);
builder.Services.AddMediatR(typeof(GetUserQueryHandler), typeof(GetUserQuery));

// Register repo
builder.Services.AddScoped<IUserRepository, UserRepository>();    //todo: move it to extension method?

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

app.UseAuthorization();

app.MapControllers();

app.Run();
