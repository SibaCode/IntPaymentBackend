using Microsoft.EntityFrameworkCore;
using IntPaymentAPI.Models;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Identity;
using IntPaymentAPI; // This is required to access ApplicationDbContext

var builder = WebApplication.CreateBuilder(args);

// Set specific URL (ensure this matches your desired port)
builder.WebHost.UseUrls("https://localhost:7150");

// Register the DbContext for Identity
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Make sure DefaultConnection is in your appsettings.json

// Register Controllers
builder.Services.AddControllers();

// Add Swagger (only in development environment)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS - Define both the "AllowAll" and "AllowReactApp" policies
builder.Services.AddCors(options =>
{
    // Policy to allow all origins, methods, and headers (for local development)
    options.AddPolicy("AllowAll", policy =>
        policy
            .AllowAnyOrigin()   // Allow any origin
            .AllowAnyMethod()   // Allow any HTTP method
            .AllowAnyHeader()); // Allow any header

    // Policy to restrict CORS to only your React app (for production)
    options.AddPolicy("AllowReactApp", policy =>
        policy
            .WithOrigins("http://localhost:3000") // React App URL
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add rate limiting
builder.Services.AddOptions();
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddInMemoryRateLimiting(); // Ensure this is set up correctly
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Use CORS policies
app.UseCors(builder.Environment.IsDevelopment() ? "AllowAll" : "AllowReactApp");  // Apply appropriate CORS policy

// Use Swagger only in dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware pipeline
app.UseIpRateLimiting();
app.UseHttpsRedirection(); // Enforce HTTPS
app.UseRouting();
app.UseAuthorization();

app.MapControllers(); // Map the controllers to endpoints

app.Run();  // Start the application
