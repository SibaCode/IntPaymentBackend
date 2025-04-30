using Microsoft.EntityFrameworkCore;
using IntPaymentAPI.Models;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Identity;
using IntPaymentAPI; // This is required to access ApplicationDbContext

var builder = WebApplication.CreateBuilder(args);

// Set specific URL
builder.WebHost.UseUrls("https://localhost:7150");

// // Register the DbContext for Identity
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Use your connection string here

// Register Controllers
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS - Define a policy for your React App and "AllowAll" policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

    // Example of a more restrictive CORS policy for your React App
    options.AddPolicy("AllowReactApp",
        policy => policy
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
builder.Services.AddInMemoryRateLimiting(); // Add this line

// // Add Identity services
// builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
// {
//     options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
//     options.Lockout.MaxFailedAccessAttempts = 5;
//     options.Lockout.AllowedForNewUsers = true;

//     options.Password.RequireDigit = true;
//     options.Password.RequiredLength = 6;
//     options.Password.RequireLowercase = true;
//     options.Password.RequireNonAlphanumeric = false;
//     options.Password.RequireUppercase = true;
//     options.Password.RequiredUniqueChars = 1;
// })
// .AddEntityFrameworkStores<ApplicationDbContext>()
// .AddDefaultTokenProviders();

var app = builder.Build();

// Use CORS policies
app.UseCors("AllowAll");
app.UseCors("AllowReactApp");  // Add this line to apply the CORS policy for the React app

// Use Swagger only in dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware pipeline
app.UseIpRateLimiting();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
