using FluentValidation;
using FluentValidation.AspNetCore;
using ScooterRentalApi.Models;
using ScooterRentalApi.Validators;

var builder = WebApplication.CreateBuilder(args);

// --- Налаштування порту для Railway ---
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(Int32.Parse(port));
});

// --- Сервіси ---
builder.Services.AddControllers()
    .AddFluentValidation();

builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RentalValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ScooterValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- Middleware ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
