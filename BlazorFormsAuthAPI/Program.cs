using BlazorFormsAuthAPI.Repositories;
using Microsoft.AspNetCore.Authentication.Negotiate;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IAccountRepository, AccountRepository>();

builder.Services.AddCors(conf => conf.AddPolicy("test", settings =>
                settings.AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins("http://localhost:5010")
                        .AllowCredentials()
            ));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("test");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
