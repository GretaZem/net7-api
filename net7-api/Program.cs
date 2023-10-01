using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.EntityFrameworkCore;
using net7_api;
using net7_api.Context;
using net7_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Keep Swagger for easier review at https://localhost:44368/swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApiDbContext>(opt =>
    opt.UseInMemoryDatabase("ApiDb"));
builder.Services.AddHttpClient();

builder.Services.AddScoped<ExternalApiService>();
builder.Services.AddScoped<DataImporter>();

builder.Services.AddHangfire(c => c.UseMemoryStorage());
builder.Services.AddHangfireServer();

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

app.UseMiddleware<LoggingMiddleware>();

app.UseHangfireServer();
app.UseHangfireDashboard("/mydashboard"); // go to https://localhost:44368/mydashboard to view queued/completed jobs

BackgroundJob.Enqueue<DataImporter>(service => service.Import()); // import once at start
RecurringJob.AddOrUpdate<DataImporter>("ImportData", service => service.Import(), Cron.Daily(1)); // then daily at 1:00AM

app.Run();

