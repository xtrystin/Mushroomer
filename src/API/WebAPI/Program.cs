using WebAPI.Controllers;
using WebAPI.Extensions;
using WebAPI.Middleware;
using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("OpenCorsPolicy", opt =>
        opt.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSingleton<IFileUploader, BlobFileUploader>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithJwtAuth();

// Register httpClients         
builder.Services.AddHttpClient<PostController>(client =>        //todo: change to httpFactory?
{
    client.BaseAddress = new Uri(builder.Configuration["MicroservicesUrl:Post"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("OpenCorsPolicy");

app.UseRequestLoggingMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
