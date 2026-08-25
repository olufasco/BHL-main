using BHL.Tournament.Data;
using BHL.Tournament.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<TournamentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Services
builder.Services.AddScoped<ResultsService>();

// CORS for React frontend (adjust origins for production)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000", policy =>
        policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod());
});

// Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowLocalhost3000");
app.MapControllers();

app.Run();
