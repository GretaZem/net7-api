using Microsoft.EntityFrameworkCore;
using net7_api.Context;
using net7_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Keep Swagger for easier review
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApiDbContext>(opt =>
    opt.UseInMemoryDatabase("ApiDb"));
builder.Services.AddHttpClient();

builder.Services.AddScoped<ExternalApiService>();

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
