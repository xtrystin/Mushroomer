using MediatR;
using Post.Application.Query;
using Post.Domain.Repository;
using Post.Infrastructure.EF;
using Post.Infrastructure.EF.Query;
using Post.Infrastructure.EF.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMSSqlServer(builder.Configuration);
builder.Services.AddMediatR(typeof(GetPostQueryHandler), typeof(GetPostQuery));

// Register repo
builder.Services.AddScoped<IPostRepository, PostRepository>();    //todo: move it to extension method?

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
