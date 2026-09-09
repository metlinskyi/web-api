using System.Text;
using Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

// Create the native AOT application builder
var builder = WebApplication.CreateSlimBuilder(args);
var config = builder.Configuration;

// Add database context
var connectionString = config.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(connectionString)); 

// Add authentication and authorization
var key = Encoding.ASCII.GetBytes(config["Jwt:Key"]!);
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MediatorPolicy",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// Add gRPC services
builder.Services.AddGrpc();
if (builder.Environment.IsDevelopment())
    builder.Services.AddGrpcReflection();

// Build the application
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
app.UseCors("MediatorPolicy");

if (app.Environment.IsDevelopment())
    app.MapGrpcReflectionService();

/// Start the application
app.Run();